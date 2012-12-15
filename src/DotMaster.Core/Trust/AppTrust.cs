using System;
using System.Collections.Generic;

namespace DotMaster.Core.Trust
{
    public class AppTrust
    {
        private readonly IDictionary<Type, TypeTrust> _trustContainersByType;

        public AppTrust() : this(new Dictionary<Type, TypeTrust>())
        {
        }

        public AppTrust(IDictionary<Type, TypeTrust> trustContainersByType)
        {
            if (trustContainersByType == null)
            {
                throw new ArgumentNullException("trustContainersByType");
            }

            _trustContainersByType = trustContainersByType;
        }

        public ITrustStrategy GetTrustStrategy(Type type, string propertyName, string source)
        {
            return _trustContainersByType.ContainsKey(type)
                       ? _trustContainersByType[type].GetTrustStrategyFor(propertyName, source)
                       : null;
        }
    }
}
