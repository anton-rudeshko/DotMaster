using System;
using System.Collections.Generic;
using DotMaster.Core.Model;
using DotMaster.Core.Trust;

namespace DotMaster.Core
{
    public class Kernel
    {
        private readonly TrustProcessor _trustProcessor;
        public IMasterDataBase MasterDB { get; set; }

        public Kernel(IMasterDataBase masterDB = null, ITrustStrategy defaultTrustStrategy = null)
        {
            MasterDB = masterDB;
            _trustProcessor = new TrustProcessor(defaultTrustStrategy);
        }

        public void Initialize()
        {
//            TrustStrategies = TypeTrustReader.ReadTrustRulesFromCurrentDomain();
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

            _trustProcessor.CalculateTrust<TKey, TBase, TXref>(baseObject);

            MasterDB.Save<TKey, TBase, TXref>(baseObject);
        }
    }
}
