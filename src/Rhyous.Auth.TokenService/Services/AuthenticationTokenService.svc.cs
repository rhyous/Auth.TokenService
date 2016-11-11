using Rhyous.Auth.TokenService.Business;
using Rhyous.Auth.TokenService.Extensions;
using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Model;
using System.Security.Authentication;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Rhyous.Auth.TokenService.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        public Token Authenticate(Credentials creds)
        {
            if (creds == null && WebOperationContext.Current != null)
            {
                var basicAuthHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (!string.IsNullOrWhiteSpace(basicAuthHeader))
                    creds = new BasicAuth(basicAuthHeader).Creds;
            }
            var credsValidator = new PluginCredentialsValidator();
            IToken token;
            if (credsValidator.IsValid(creds, out token))
                return token.ToConcrete<Token>();
            else
                throw new AuthenticationException("Invalid credentials.");
        }
    }
}
