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
    }
}