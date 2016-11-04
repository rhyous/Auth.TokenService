using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Model;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace DatabaseAuthenticator
{
    public class DatabaseTokenBuilder : ITokenBuilder
    {
        public static int TokenSize = 100;
        private readonly BasicTokenDbContext _DbContext;

        public DatabaseTokenBuilder(BasicTokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IToken Build(Credentials creds)
        {
            var user = _DbContext.Users.SingleOrDefault(u => u.Username.Equals(creds.User, StringComparison.CurrentCultureIgnoreCase));
            var tokenText = BuildSecureToken(TokenSize);
            var token = new Token { Text = tokenText, User = user, CreateDate = DateTime.Now };
            _DbContext.Tokens.Add(token);
            _DbContext.SaveChanges();
            return token;
        }

        private string BuildSecureToken(int length)
        {
            var buffer = new byte[length];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetNonZeroBytes(buffer);
            }
            return Convert.ToBase64String(buffer);
        }
    }
}