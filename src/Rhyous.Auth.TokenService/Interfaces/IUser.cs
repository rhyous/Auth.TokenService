using System.Collections.Generic;

namespace Rhyous.Auth.TokenService.Interface
{
    public interface IUser
    {
        int Id { get; set; }
        string Password { get; set; }
        string Salt { get; set; }
        ICollection<IToken> Tokens { get; set; }
        string Username { get; set; }
    }
}