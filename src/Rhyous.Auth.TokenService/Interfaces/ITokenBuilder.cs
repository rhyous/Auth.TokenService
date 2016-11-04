using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Model;

namespace Rhyous.Auth.TokenService.Interfaces
{
    public interface ITokenBuilder
    {
        IToken Build(Credentials creds);
    }
}
