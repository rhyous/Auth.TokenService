using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Rhyous.Auth.TokenService.Behaviors
{
    public class TokenValidationServiceBehavior : ServiceBehaviorBase
    {
        public override void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var t in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = t as ChannelDispatcher;
                if (channelDispatcher != null)
                {
                    foreach (var endpointDispatcher in channelDispatcher.Endpoints)
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new TokenValidationInspector());
                    }
                }
            }
        }
    }
}
