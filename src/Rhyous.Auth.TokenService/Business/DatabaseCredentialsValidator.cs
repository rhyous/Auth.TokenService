using System;
using System.Linq;
using Rhyous.Auth.TokenService.Database;
using Rhyous.Auth.TokenService.Interfaces;

namespace Rhyous.Auth.TokenService.Business
{
    public class DatabaseCredentialsValidator : ICredentialsValidator
    {
        private readonly BasicTokenDbContext _DbContext;

        public DatabaseCredentialsValidator(BasicTokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public bool IsValid(Model.Credentials creds)
        {
            var user = _DbContext.Users.SingleOrDefault(u => u.Username.Equals(creds.User, StringComparison.CurrentCultureIgnoreCase));
            return user != null && Hash.Compare(creds.Password, user.Salt, user.Password, Hash.DefaultHashType, Hash.DefaultEncoding);
        }
    }
}