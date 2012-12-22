using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DotMaster.Core.Model;
using DotMaster.Core.Trust.Strategies;
using DotMaster.Core.Utils;

namespace DotMaster.Core.Trust
{
    public class TrustProcessor
    {
        public AppTrust AppTrust { get; set; }

        public ITrustStrategy DefaultTrustStrategy { get; set; }

        public TrustProcessor()
            : this(new AppTrustReader()) {}

        public TrustProcessor(AppTrustReader appTrustReader)
            : this(appTrustReader.ReadTrustRulesFromCurrentDomain()) {}

        public TrustProcessor(AppTrust appTrust)
        {
            if (appTrust == null)
            {
                throw new ArgumentNullException("appTrust");
            }

            AppTrust = appTrust;
            DefaultTrustStrategy = new FixedScoreTrustStrategy(0);
        }

        public void CalculateTrust<K, B, X>(B baseObject)
            where B : class, IBaseObject<K, B, X>
            where X : class, ICrossReference<K, B, X>
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            Debug.WriteLine("Calculating trust for " + baseObject);

            Debug.Assert(baseObject.Xrefs != null);
            Debug.Assert(baseObject.Xrefs.Count > 0);

            foreach (var property in ReflectionUtils.GetMasteredProperties(typeof (B)))
            {
                Debug.WriteLine("Processing property " + property.Name);
                var mostTrusted = GetMostTrustedXref<K, B, X>(baseObject, property);
                ReflectionUtils.Copy(property, @from: mostTrusted.ObjectData, to: baseObject);
            }

            baseObject.LastUpdate = baseObject.Xrefs.Max(x => x.LastUpdate);
        }

        private X GetMostTrustedXref<K, B, X>(B baseObject, PropertyInfo property)
            where B : class, IBaseObject<K, B, X>
            where X : class, ICrossReference<K, B, X>
        {
            Debug.Assert(baseObject.Xrefs.Count > 0);

            // Считаем траст для всех xref и группируем по нему
            // Берём группу с наибольшим трастом
            // Берём наиболее свежий xref из группы
            return baseObject.Xrefs
                             .GroupBy(xref => CalculateTrust<K, B, X>(property, baseObject, xref))
                             .OrderByDescending(group => @group.Key).First()
                             .OrderByDescending(xref => xref.LastUpdate).First();
        }

        private int CalculateTrust<K, B, X>(PropertyInfo property, B baseObject, X xref)
            where B : class, IBaseObject<K, B, X>
            where X : class, ICrossReference<K, B, X>
        {
            return GetTrustStrategy<K, B, X>(property, xref).GetXrefScore<K, B, X>(baseObject, xref);
        }

        private ITrustStrategy GetTrustStrategy<K, B, X>(PropertyInfo property, X xref) 
            where B : class, IBaseObject<K, B, X> 
            where X : class, ICrossReference<K, B, X>
        {
            return AppTrust.GetTrustStrategy(typeof(B), property.Name, xref.Source) ?? DefaultTrustStrategy;
        }
    }
}