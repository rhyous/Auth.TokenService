using System;

namespace Rhyous.Auth.TokenService.Extensions
{
    public static class BasicAuthExtensions
    {
        public static bool Compare(this Credentials validCreds, Credentials offerredCreds)
        {
            return validCreds.User.Equals(offerredCreds.User, StringComparison.OrdinalIgnoreCase)
                && validCreds.Password == offerredCreds.Password;
        }
    }
}
