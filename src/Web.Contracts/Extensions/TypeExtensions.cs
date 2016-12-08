namespace Web.Contracts.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class TypeExtensions
    {
        public static bool ContainsAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttributes(true).Any(x => x != null && x.GetType() == typeof(T));
        }
    }
}