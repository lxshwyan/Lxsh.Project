using System;
using System.Reflection;

namespace Lxsh.Project.Common
{
    public static class AttributeExtension
    {
        public static T GetAttribute<T>(this object obj) where T : class
        {
            Type type = obj.GetType();
            return type.GetAttribute<T>();
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            Attribute customAttribute = type.GetCustomAttribute(typeof(T));
            T result;
            if (customAttribute.IsNotNull())
            {
                result = (customAttribute as T);
            }
            else
            {
                result = default(T);
            }
            return result;
        }
    }
}
