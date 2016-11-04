using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Model;

namespace Rhyous.Auth.TokenService.Interfaces
{
    public interface ICredentialsValidator : ITokenBuilder
    {
        bool IsValid(Credentials creds, out IToken token);
    }
}