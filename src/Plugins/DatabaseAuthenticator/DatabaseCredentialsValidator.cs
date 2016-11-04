using Rhyous.Auth.TokenService.Business;
using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Model;
using System;
using System.Linq;

namespace DatabaseAuthenticator
{
    public class DatabaseCredentialsValidator : ICredentialsValidator
    {
        private readonly BasicTokenDbContext _DbContext;

        public DatabaseCredentialsValidator() : this(new BasicTokenDbContext())
        {
        }

        public DatabaseCredentialsValidator(BasicTokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IToken Build(Credentials creds)
        {
            return new DatabaseTokenBuilder(_DbContext).Build(creds);
        }

        public bool IsValid(Credentials creds, out IToken token)
        {
            var user = _DbContext.Users.SingleOrDefault(u => u.Username.Equals(creds.User, StringComparison.CurrentCultureIgnoreCase));
            var result = user != null && Hash.Compare(creds.Password, user.Salt, user.Password, Hash.DefaultHashType, Hash.DefaultEncoding);
            token = result ? Build(creds) : null;
            return result;
        }
    }
}