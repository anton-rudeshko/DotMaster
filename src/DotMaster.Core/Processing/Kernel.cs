using System;
using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Processing
{
    public class Kernel
    {
        public Kernel()
        {
        }

        public Kernel(IMasterDataBase masterDB)
        {
            MasterDB = masterDB;
        }

        public IMasterDataBase MasterDB { get; set; }

        public void RegisterDataProvider<TKey, TBase, TXref>(ISourceDataProvider<TKey, TBase, TXref> dataProvider)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }
            dataProvider.OnData += Process<TKey, TBase, TXref>;
        }

        private void Process<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            // Ignore null xrefs
            if (xref == null)
            {
                return;
            }
            TXref presentXref;
            var baseObject = TryGetXref<TKey, TBase, TXref>(xref, out presentXref) ? presentXref.BaseObject : null /*new TBase()*/;
            UpdateBaseObjectFromXref<TKey, TBase, TXref>(baseObject, xref);
        }

        private void UpdateBaseObjectFromXref<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            // calculate trust rules
            // todo: how to handle BO deletion?
            // copy fields from xref.ObjectData to baseObject
        }

        private TBase LoadBaseObjectFor<TKey, TBase, TXref>(TXref xref)
        {
            throw new NotImplementedException();
        }

        private bool TryGetXref<TKey, TBase, TXref>(TXref xref, out TXref presentXref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            presentXref = MasterDB.QueryForXref<TKey, TBase, TXref>(xref.SourceKey, xref.Source);
            return presentXref == null;
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