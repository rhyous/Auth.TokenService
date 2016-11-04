using System;
using System.Linq;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Interface;

namespace DatabaseAuthenticator
{
    public class DatabaseTokenValidator : ITokenValidator
    {
        // Todo: Set this from a web.config appSettting value
        public static double DefaultSecondsUntilTokenExpires = 1800;

        private readonly BasicTokenDbContext _DbContext;

        public DatabaseTokenValidator() : this(new BasicTokenDbContext())
        {
        }

        public DatabaseTokenValidator(BasicTokenDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IToken Token { get; set; }

        public bool IsValid(string tokentext)
        {
            Token = _DbContext.Tokens.SingleOrDefault(t => t.Text == tokentext);
            return Token != null && !IsExpired(Token);
        }

        internal bool IsExpired(IToken token)
        {
            var span = DateTime.Now - token.CreateDate;
            return span.TotalSeconds > DefaultSecondsUntilTokenExpires;
        }
    }
}