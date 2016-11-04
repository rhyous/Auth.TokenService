using Rhyous.Auth.TokenService.Interface;
using Rhyous.Auth.TokenService.Interfaces;
using Rhyous.SimplePluginLoader;
using System;

namespace Rhyous.Auth.TokenService.Business
{
    class PluginTokenValidator : PluginLoaderBase<ITokenValidator>, ITokenValidator
    {
        public IToken Token { get; set; }

        public override string PluginSubFolder => "Authenticators";

        private PluginCollection<ITokenValidator> GetPlugins()
        {
            var plugins = PluginLoader.LoadPlugins();
            if (plugins == null || plugins.Count == 0)
                throw new Exception("No authenticator plugin not found.");
            return plugins;
        }

        public bool IsValid(string token)
        {
            foreach (var plugin in Plugins)
            {
                if (plugin.IsValid(token))
                {
                    Token = plugin.Token;
                    return true;
                }
            }
            return false;
        }
    }
}
