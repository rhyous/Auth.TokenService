using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace Rhyous.Auth.TokenService.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Test1Service
    {
        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string TestPost()
        {
            return string.Format("Your authentication worked! User: {0} User Id: {1}",
                WebOperationContext.Current.IncomingRequest.Headers["User"],
                WebOperationContext.Current.IncomingRequest.Headers["UserId"]);
        }

        [OperationContract]
        [WebInvoke(Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string TestGet()
        {
            return string.Format("Your authentication worked! User: {0} User Id: {1}",
                WebOperationContext.Current.IncomingRequest.Headers["User"],
                WebOperationContext.Current.IncomingRequest.Headers["UserId"]);
        }
    }
}
