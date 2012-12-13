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

        private void RecalculateTrust<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            Debug.Assert(baseObject != null);
            Debug.Assert(baseObject.Xrefs != null);
            Debug.Assert(baseObject.Xrefs.Count > 0);

            foreach (var property in ReflectionUtils.GetMasteredProperties(typeof (TBase)))
            {
                Debug.WriteLine("Processing property " + property.Name);
                var mostTrusted = baseObject.Xrefs[0];

                foreach (var xref in baseObject.Xrefs)
                {
                    var trustStrategy = GetTrustStrategy<TKey, TBase, TXref>(property, xref.Source);
                    // todo: calc trust
                }

                CopyValue<TKey, TBase, TXref>(property, @from: mostTrusted.ObjectData, to: baseObject);
            }

            baseObject.LastUpdate = baseObject.Xrefs.Max(x => x.LastUpdate);

            // todo: how to handle BO deletion?
            // если нет траста, обновляем поле по LUD

            MasterDB.Save<TKey, TBase, TXref>(baseObject);
        }

        private static void CopyValue<TKey, TBase, TXref>(PropertyInfo property, TBase @from, TBase to)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            property.SetValue(to, property.GetValue(@from, null), null);
        }

        private ITrustStrategy GetTrustStrategy<TKey, TBase, TXref>(PropertyInfo property, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            Debug.Assert(source != null);

            return TrustStrategies.ContainsKey(property) ? TrustStrategies[property] : DefaultTrustStrategy;
        }

        private TBase LoadBaseObjectFor<TKey, TBase, TXref>(TXref xref)
        {
            throw new NotImplementedException();
        }

        private TBase CreateNewBase<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            TBase newBaseObject = null;
            // todo: copy fields from xref to BO
            return newBaseObject;
        }

        private void Save<TKey, TBase, TXref>(TXref xref)
            where TXref : class, ICrossReference<TKey, TBase, TXref>
            where TBase : class, IBaseObject<TKey, TBase, TXref>
        {
            throw new System.NotImplementedException();
        }

        private void UpdateXref<TKey, TBase, TXref>(TXref presentXref, TXref xref)
            where TXref : class, ICrossReference<TKey, TBase, TXref>
            where TBase : class, IBaseObject<TKey, TBase, TXref>
        {
            throw new System.NotImplementedException();
        }
    }
}
