using Rhyous.Auth.TokenService.Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Rhyous.Auth.TokenService.Interfaces
{
    [ServiceContract]
    public interface IAuthenticationTokenService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Token Authenticate(Credentials creds);
    }
}
