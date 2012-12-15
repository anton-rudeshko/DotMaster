using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Utils;

namespace DotMaster.Core.Trust
{
    public class TrustProcessor
    {
        public IDictionary<PropertyInfo, IDictionary<string, ITrustStrategy>> TrustStrategies { get; set; }

        public TrustReader TrustReader { get; set; }

        public ITrustStrategy DefaultTrustStrategy { get; set; }

        public TrustProcessor(ITrustStrategy defaultTrustStrategy)
        {
            DefaultTrustStrategy = defaultTrustStrategy ?? new FixedScoreTrustStrategy(0);
            TrustStrategies = new Dictionary<PropertyInfo, IDictionary<string, ITrustStrategy>>();
            TrustReader = new TrustReader();
        }

        public void CalculateTrust<K, B, X>(B baseObject)
            where B : class, IBaseObject<K, B, X>
            where X : class, ICrossReference<K, B, X>
        {
            Debug.WriteLine("Calculating trust for " + baseObject);
            Debug.Assert(baseObject != null);
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
            return GetTrustStrategy<K, B, X>(property, xref.Source).GetScore<K, B, X>(baseObject, xref);
        }

        private ITrustStrategy GetTrustStrategy<K, B, X>(PropertyInfo property, string source)
            where B : class, IBaseObject<K, B, X>
            where X : class, ICrossReference<K, B, X>
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(source), "Source should be not empty");

            return TrustStrategies.ContainsKey(property) && TrustStrategies[property].ContainsKey(source)
                       ? TrustStrategies[property][source]
                       : DefaultTrustStrategy;
        }
    }
}