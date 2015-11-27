using System;
using System.ServiceModel.Configuration;

namespace WcfSimpleTokenExample.Behaviors
{
    public class TokenValidationBehaviorExtension : BehaviorExtensionElement
    {
        #region BehaviorExtensionElement

        public override Type BehaviorType
        {
            get { return typeof(TokenValidationServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new TokenValidationServiceBehavior();
        }

        #endregion
    }
}
