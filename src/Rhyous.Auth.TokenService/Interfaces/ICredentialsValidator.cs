using Rhyous.Auth.TokenService.Model;

namespace Rhyous.Auth.TokenService.Interfaces
{
    public interface ICredentialsValidator
    {
        bool IsValid(Credentials creds);
    }
}