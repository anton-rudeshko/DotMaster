using System;
using System.Reflection;

namespace DotMaster.Core.Utils
{
    static internal class ReflectionUtils
    {
        public static PropertyInfo[] GetMasteredProperties(Type type) 
        {
            return type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        }

        public static void Copy(PropertyInfo property, object @from, object to)
        {
            property.SetValue(to, property.GetValue(@from, null), null);
        }
    }
}