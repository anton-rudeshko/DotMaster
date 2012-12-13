using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Trust;
using DotMaster.Core.Utils;

namespace DotMaster.Core.Processing
{
    public class Kernel
    {
        public IMasterDataBase MasterDB { get; set; }
        public ITrustStrategy DefaultTrustStrategy { get; set; }

        protected IDictionary<PropertyInfo, ITrustStrategy> TrustStrategies { get; set; }
        protected TrustReader TrustReader { get; set; }

        public Kernel(IMasterDataBase masterDB = null, ITrustStrategy defaultTrustStrategy = null)
        {
            MasterDB = masterDB;
            DefaultTrustStrategy = defaultTrustStrategy ?? new LastUpdateDateTrustStrategy();
                // todo: default trust strategy

            TrustStrategies = new Dictionary<PropertyInfo, ITrustStrategy>();
            TrustReader = new TrustReader();
        }

        public void Initialize()
        {
            TrustStrategies = TrustReader.ReadTrustRulesFromCurrentDomain();
        }

        public void RegisterDataProvider<TKey, TBase, TXref>(ISourceDataProvider<TKey, TBase, TXref> dataProvider)
            where TBase : class, IBaseObject<TKey, TBase, TXref>, new() 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }
            dataProvider.OnData += Process<TKey, TBase, TXref>;
        }

        public void Process<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>, new()
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (xref == null)
            {
                throw new ArgumentNullException("xref");
            }
            if (string.IsNullOrWhiteSpace(xref.Source))
            {
                throw new ArgumentException(I18n.XrefSourceIsEmpty, "xref");
            }
            if (string.IsNullOrWhiteSpace(xref.SourceKey))
            {
                throw new ArgumentException(I18n.XrefSourceKeyIsEmpty, "xref");
            }

            // Update present xref if any
            var presentXref = MasterDB.QueryForXref<TKey, TBase, TXref>(xref.SourceKey, xref.Source);
            TBase baseObject;
            if (presentXref != null)
            {
                presentXref.ObjectData = xref.ObjectData;
                presentXref.LastUpdate = xref.LastUpdate;
                baseObject = presentXref.BaseObject;
            }
            else
            {
                baseObject = new TBase { Xrefs = new List<TXref> { xref } };
            }

            RecalculateTrust<TKey, TBase, TXref>(baseObject);
        }

        private void RecalculateTrust<K, B, X>(B baseObject)
            where B : class, IBaseObject<K, B, X>
            where X : class, ICrossReference<K, B, X>
        {
            Debug.Assert(baseObject != null);
            Debug.Assert(baseObject.Xrefs != null);
            Debug.Assert(baseObject.Xrefs.Count > 0);

            foreach (var property in ReflectionUtils.GetMasteredProperties(typeof (B)))
            {
                Debug.WriteLine("Processing property " + property.Name);
                var mostTrusted = GetMostTrustedXref<K, B, X>(baseObject, property);
                Copy(property, @from: mostTrusted.ObjectData, to: baseObject);
            }

            baseObject.LastUpdate = baseObject.Xrefs.Max(x => x.LastUpdate);

            // todo: how to handle BO deletion?
            // если нет траста, обновляем поле по LUD

            MasterDB.Save<K, B, X>(baseObject);
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
                .OrderByDescending(group => group.Key).First()
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
            Debug.Assert(string.IsNullOrWhiteSpace(source), "Source should be not empty");

            return TrustStrategies.ContainsKey(property) ? TrustStrategies[property] : DefaultTrustStrategy;
        }

        private static void Copy(PropertyInfo property, object @from, object to)
        {
            property.SetValue(to, property.GetValue(@from, null), null);
        }
    }
}
