using System;
using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.Auth.TokenService.Model;

namespace Rhyous.Auth.TokenService.Business
{
    public class PluginCredentialsValidator : PluginLoaderBase<ICredentialsValidator>, ICredentialsValidator
    {
        public override string PluginSubFolder => "Authenticators";

        public bool IsValid(Credentials creds, out IToken token)
        {
            foreach (var plugin in Plugins)
            {
                if (plugin.IsValid(creds, out token))
                {
                    return true;
                }
            }
            token = null;
            return false;
        }

        public IToken Build(Credentials creds)
        {
            throw new NotImplementedException();
        }
    }
}