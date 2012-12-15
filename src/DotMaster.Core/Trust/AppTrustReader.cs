using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotMaster.Core.Model;

namespace DotMaster.Core.Trust
{
    public class AppTrustReader
    {
        private readonly TypeTrustReader _typeTrustReader;

        private static readonly Type BaseObjectInterfaceType = typeof (IBaseObject<,,>);

        public AppTrustReader() : this(new TypeTrustReader())
        {
        }

        public AppTrustReader(TypeTrustReader typeTrustReader)
        {
            if (typeTrustReader == null)
            {
                throw new ArgumentNullException("typeTrustReader");
            }
            _typeTrustReader = typeTrustReader;
        }

        public AppTrust ReadTrustRulesFromCurrentDomain()
        {
            return ReadTrustRulesFrom(AppDomain.CurrentDomain);
        }

        public AppTrust ReadTrustRulesFrom(AppDomain domain)
        {
            return ReadTrustRulesFrom(domain.GetAssemblies());
        }

        public AppTrust ReadTrustRulesFrom(IEnumerable<Assembly> assemblies)
        {
            return ReadTrustRulesFrom(assemblies.SelectMany(MasteredTypes));
        }

        public AppTrust ReadTrustRulesFrom(IEnumerable<Type> types)
        {
            return new AppTrust(types.ToDictionary(type => type, _typeTrustReader.ReadAllTrustRulesFrom));
        }

        public IEnumerable<Type> MasteredTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(TypeIsMastered);
        }

        public bool TypeIsMastered(Type type)
        {
            return type.GetInterfaces().Any(DefinedFromBaseObject);
        }

        private static bool DefinedFromBaseObject(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == BaseObjectInterfaceType;
        }
    }
}