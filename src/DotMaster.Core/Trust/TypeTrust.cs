using System;
using System.Collections.Generic;

namespace DotMaster.Core.Trust
{
    public class TypeTrust
    {
        private readonly IDictionary<string, MemberTrust> _trustByProperties;
        private readonly MemberTrust _classLevel;

        public MemberTrust ClassLevel
        {
            get { return _classLevel; }
        }

        public TypeTrust() : this(new Dictionary<string, MemberTrust>(), new MemberTrust())
        {
        }

        public TypeTrust(IDictionary<string, MemberTrust> trustByProperties, MemberTrust classLevel)
        {
            if (trustByProperties == null)
            {
                throw new ArgumentNullException("trustByProperties");
            }

            _trustByProperties = trustByProperties;
            _classLevel = classLevel;
        }

        public ITrustStrategy GetTrustStrategyFor(string propertyName, string source)
        {
            var memberTrust = _trustByProperties.ContainsKey(propertyName) ? _trustByProperties[propertyName] : ClassLevel;
            return memberTrust == null ? null : memberTrust.GetTrustStrategyFor(source);
        }
    }
}