using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotMaster.Core.Trust
{
    public class TypeTrustReader
    {
        private static readonly IList<string> Ignored = new[] { "ObjKey", "Xrefs" }; // todo: may be use lambda

        private readonly MemberTrustReader _memberTrustReader;

        public TypeTrustReader() : this(new MemberTrustReader())
        {
        }

        public TypeTrustReader(MemberTrustReader memberTrustReader)
        {
            _memberTrustReader = memberTrustReader;
        }

        public TypeTrust ReadAllTrustRulesFrom(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var trustByProperties = new Dictionary<string, MemberTrust>();
            var properties = type.GetProperties().Where(NotIgnored);
            foreach (var property in properties)
            {
                var trustContainer = ReadTrustRulesFrom(property);
                if (trustContainer != null)
                {
                    trustByProperties.Add(property.Name, trustContainer);
                }
            }

            return new TypeTrust(trustByProperties, ReadTrustRulesFrom(type));
        }

        public MemberTrust ReadTrustRulesFrom(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            try
            {
                return _memberTrustReader.ReadTrust(type);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("Exception during reading trust rules for " + type.FullName, e);
            }
        }

        public MemberTrust ReadTrustRulesFrom(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }

            try
            {
                return _memberTrustReader.ReadTrust(propertyInfo);
            }
            catch (InvalidOperationException e)
            {
                var memberName = (propertyInfo.DeclaringType == null ? "" : propertyInfo.DeclaringType.FullName) + " " + propertyInfo.Name;
                throw new InvalidOperationException("Exception during reading trust rules for " + memberName, e);
            }
        }

        private static bool NotIgnored(PropertyInfo propertyInfo)
        {
            return !Ignored.Contains(propertyInfo.Name);
        }
    }
}
