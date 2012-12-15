using System;
using System.Collections.Generic;

namespace DotMaster.Core.Trust
{
    public class AppTrust
    {
        private readonly IDictionary<Type, TypeTrust> _trustContainersByType;

        public AppTrust(IDictionary<Type, TypeTrust> trustContainersByType)
        {
            _trustContainersByType = trustContainersByType;
        }
    }
}