using Rhyous.Auth.TokenService.Interface;
using System;

namespace Rhyous.Auth.TokenService.Extensions
{
    public static class ConcreteConverter
    {
        public static T ToConcrete<T, Tinterface>(this Tinterface item, Func<T> copyProperties)
            where T : class, Tinterface, new()
        {
            if (item == null)
                return default(T);
            if (typeof(T).IsAssignableFrom(item.GetType()))
                return item as T;
            var concreteItem = new T();
            if (copyProperties != null)
                return copyProperties.Invoke();
            return default(T);
        }

        public static T ToConcrete<T>(this IToken item)
            where T : class, IToken, new()
        {
            return item.ToConcrete(() =>
             {
                 return new T
                 {
                     Id = item.Id,
                     Text = item.Text,
                     User = item.User,
                     UserId = item.UserId
                 };
             });
        }

        public static T ToConcrete<T>(this IUser item)
            where T : class, IUser, new()
        {
            return item.ToConcrete(() =>
            {
                return new T
                {
                    Id = item.Id,
                    Password = item.Password,
                    Salt = item.Salt,
                    Tokens = item.Tokens,
                    Username = item.Username
                };
            });
        }
    }
}
