using System;
using System.Linq.Expressions;
using System.Reflection;

namespace DotMaster.Core.Utils
{
    public static class ReflectionUtils
    {
        public static PropertyInfo[] GetMasteredProperties(Type type)
        {
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        }

        public static void Copy(PropertyInfo property, object @from, object to)
        {
            property.SetValue(to, property.GetValue(@from, null), null);
        }

        public static string NameOf<TSource>(Expression<Func<TSource, object>> selector)
        {
            var type = typeof (TSource);

            var member = selector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", selector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", selector));
            }

            if (type != propInfo.ReflectedType && type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Expresion '{0}' refers to a property that is not from type {1}.", selector, type));
            }

            return propInfo.Name;
        }
    }
}
