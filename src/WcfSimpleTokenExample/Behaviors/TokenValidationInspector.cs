using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using WcfSimpleTokenExample.Business;
using WcfSimpleTokenExample.Database;
using WcfSimpleTokenExample.Interfaces;
using WcfSimpleTokenExample.Services;

namespace WcfSimpleTokenExample.Behaviors
{
    public class TokenValidationInspector : IDispatchMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            // Return BadRequest if request is null
            if (WebOperationContext.Current == null) { throw new WebFaultException(HttpStatusCode.BadRequest); }

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
                WebOperationContext.Current.IncomingRequest.Headers.Add("User", validator.Token.User.Username);
                WebOperationContext.Current.IncomingRequest.Headers.Add("UserId", validator.Token.User.Id.ToString());
            }
        }

        private static void ValidateBasicAuthentication()
        {
            var authorization = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
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