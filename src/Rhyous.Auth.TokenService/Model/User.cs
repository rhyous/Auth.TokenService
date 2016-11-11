using Rhyous.Auth.TokenService.Interface;
using System.Collections.Generic;

namespace Rhyous.Auth.TokenService.Model
{
    public partial class User : IUser
    {
        public User()
        {
            Tokens = new HashSet<IToken>();
        }

        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Salt { get; set; }

        public virtual ICollection<IToken> Tokens { get; set; }
    }
}
