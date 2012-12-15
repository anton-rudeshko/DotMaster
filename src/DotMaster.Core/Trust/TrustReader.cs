using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotMaster.Core.Model;
using DotMaster.Core.Trust.Attributes;

namespace DotMaster.Core.Trust
{
    public class TrustReader
    {
        private static readonly IList<string> Ignored = new[] { "ObjKey", "Xrefs" }; // todo: may be use lambda
        private static readonly Type BaseObjectInterfaceType = typeof (IBaseObject<,,>);

        public IDictionary<PropertyInfo, ITrustStrategy> ReadTrustRulesFromCurrentDomain()
        {
            return ReadTrustRulesFrom(AppDomain.CurrentDomain);
        }

        public IDictionary<PropertyInfo, ITrustStrategy> ReadTrustRulesFrom(AppDomain domain)
        {
            return ReadTrustRulesFrom(domain.GetAssemblies());
        }

        public IDictionary<PropertyInfo, ITrustStrategy> ReadTrustRulesFrom(IEnumerable<Assembly> assemblies)
        {
            return ReadTrustRulesFrom(assemblies.SelectMany(MasteredTypes));
        }

        public IDictionary<PropertyInfo, ITrustStrategy> ReadTrustRulesFrom(IEnumerable<Type> types)
        {
            return types.SelectMany(ReadTrustRulesFrom).ToDictionary(r => r.Key, r => r.Value);
        }

        public IDictionary<PropertyInfo, ITrustStrategy> ReadTrustRulesFrom(Type type)
        {
            var dictionary = new Dictionary<PropertyInfo, ITrustStrategy>();
            foreach (var propertyInfo in type.GetProperties().Where(NotIgnored))
            {
                var trustStrategy = ReadTrustRulesFrom(propertyInfo);
                if (trustStrategy != null)
                {
                    dictionary.Add(propertyInfo, trustStrategy);
                }
            }
            return dictionary;
        }

        public ITrustStrategy ReadTrustRulesFrom(PropertyInfo propertyInfo)
        {
            var trustStrategyAttribute = propertyInfo.GetCustomAttributes(true).OfType<AbstractTrustStrategyAttribute>().FirstOrDefault();
            return trustStrategyAttribute == null ? null : trustStrategyAttribute.GetStrategyInstance();
        }

        public IEnumerable<Type> MasteredTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(TypeIsMastered);
        }

        public bool TypeIsMastered(Type type)
        {
            return type.GetInterfaces().Any(DefinedFromBaseObject);
        }

        private bool DefinedFromBaseObject(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == BaseObjectInterfaceType;
        }

        private bool NotIgnored(PropertyInfo propertyInfo)
        {
            return !Ignored.Contains(propertyInfo.Name);
        }
    }

    internal class TrustRule
    {
    }

    public class TrustContainer
    {
    }
}
