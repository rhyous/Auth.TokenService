using Rhyous.Auth.TokenService.Interface;

namespace Rhyous.Auth.TokenService.Interfaces
{
    public interface ITokenValidator
    {
        bool IsValid(string token);
        IToken Token { get; set; }
    }
}