using System.Configuration;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using Rhyous.Auth.TokenService.Business;
using Rhyous.Auth.TokenService.Database;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Services;
using Rhyous.Extensions;

namespace Rhyous.Auth.TokenService.Behaviors
{
    public class TokenValidationInspector : IDispatchMessageInspector
    {
        public static readonly string AllowAnonymousSvcPages = "AllowAnonymousSvcPages";
        public static readonly string AllowAnonymousSvcHelpPages = "AllowAnonymousSvcHelpPages";
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Return BadRequest if request is null
            if (WebOperationContext.Current == null) { throw new WebFaultException(HttpStatusCode.BadRequest); }

            if (IsAnonymousAllowed(request.Headers.To.AbsolutePath))
                return null;

            // Get Token from header
            var token = WebOperationContext.Current.IncomingRequest.Headers["Token"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                ValidateToken(token);
            }
            else
            {
                ValidateBasicAuthentication();
            }
            return null;
        }

        private static bool IsAnonymousAllowed(string absolutePath)
        {
            return (ConfigurationManager.AppSettings.Get(AllowAnonymousSvcPages, true)
                    && absolutePath.EndsWith(".svc"))
                   || (ConfigurationManager.AppSettings.Get(AllowAnonymousSvcHelpPages, true)
                       && absolutePath.Contains("/help"));
        }

        private static void ValidateToken(string token)
        {
            using (var dbContext = new BasicTokenDbContext())
            {
                ITokenValidator validator = new DatabaseTokenValidator(dbContext);
                if (!validator.IsValid(token))
                {
                    throw new WebFaultException(HttpStatusCode.Forbidden);
                }
                // Add User ids to the header so the service has them if needed
                WebOperationContext.Current?.IncomingRequest.Headers.Add("User", validator.Token.User.Username);
                WebOperationContext.Current?.IncomingRequest.Headers.Add("UserId", validator.Token.User.Id.ToString());
            }
        }

        private static void ValidateBasicAuthentication()
        {
            var authorization = WebOperationContext.Current?.IncomingRequest.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(authorization))
            {
                throw new WebFaultException(HttpStatusCode.Forbidden);
            }
            var basicAuth = new BasicAuth(authorization);
            var token = new AuthenticationTokenService().Authenticate(basicAuth.Creds);
            ValidateToken(token);
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }
    }
}