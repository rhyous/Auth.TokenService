﻿using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Rhyous.Auth.TokenService.Business;
using Rhyous.Auth.TokenService.Database;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Model;

namespace Rhyous.Auth.TokenService.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        public string Authenticate(Credentials creds)
        {
            if (creds == null && WebOperationContext.Current != null)
            {
                var basicAuthHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
                if (!string.IsNullOrWhiteSpace(basicAuthHeader))
                    creds = new BasicAuth(basicAuthHeader).Creds;
            }
            using (var dbContext = new BasicTokenDbContext())
            {
                return new DatabaseTokenBuilder(dbContext).Build(creds);
            }
        }
    }
}
