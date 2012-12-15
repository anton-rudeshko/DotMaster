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
            if (classLevel == null)
            {
                throw new ArgumentNullException("classLevel");
            }

            _trustByProperties = trustByProperties;
            _classLevel = classLevel;
        }

        public ITrustStrategy GetTrustStrategyFor(string propertyName, string source)
        {
            return GetTrustContainerFor(propertyName).GetTrustStrategyFor(source);
        }

        public MemberTrust GetTrustContainerFor(string propertyName)
        {
            return _trustByProperties.ContainsKey(propertyName) ? _trustByProperties[propertyName] : ClassLevel;
        }
    }
}