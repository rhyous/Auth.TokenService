using Rhyous.Auth.TokenService.Database;

namespace Rhyous.Auth.TokenService.Interfaces
{
    public interface ITokenValidator
    {
        bool IsValid(string token);
        Token Token { get; set; }
    }
}