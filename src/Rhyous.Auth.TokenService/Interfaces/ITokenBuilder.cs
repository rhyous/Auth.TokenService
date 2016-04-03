using Rhyous.Auth.TokenService.Model;

namespace Rhyous.Auth.TokenService.Interfaces
{
    interface ITokenBuilder
    {
        string Build(Credentials creds);
    }
}
