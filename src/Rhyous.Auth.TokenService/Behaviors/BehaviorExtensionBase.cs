using System;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace Rhyous.Auth.TokenService.Behaviors
{
    public abstract class BehaviorExtensionBase<T> : BehaviorExtensionElement
        where T : IServiceBehavior, new()
    {
        public override Type BehaviorType => typeof(T);

        protected override object CreateBehavior()
        {
            return new T();
        }
    }
}