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

        public void RegisterDataProvider<TBase, TXref>(ISourceDataProvider<TBase, TXref> dataProvider)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }
            dataProvider.OnData += Process<TBase, TXref>;
        }

        private void Process<TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>
        {
            // todo: fix date
            if (xref.UpdateDate == null)
            {
                xref.UpdateDate = new DateTime();
            }

            TXref presentXref;
            var baseObject = TryGetXref<TBase, TXref>(xref, out presentXref) ? presentXref.BaseObject : null;
            UpdateBaseObject(baseObject, xref);
        }

        private void UpdateBaseObject<TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>
        {
            baseObject.LastUpdate = xref.UpdateDate;
            throw new NotImplementedException();
        }

        private TBase LoadBaseObjectFor<TBase, TXref>(TXref xref)
        {
            throw new NotImplementedException();
        }

        private bool TryGetXref<TBase, TXref>(TXref xref, out TXref presentXref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>
        {
            presentXref = MasterDB.QueryForXref<TBase, TXref>(xref.SourceKey, xref.Source);
            return presentXref == null;
        }

        private TBase CreateNewBase<TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>
        {
            TBase newBaseObject = null;
            // todo: copy fields from xref to BO
            return newBaseObject;
        }

        private void Save<TBase, TXref>(TXref xref)
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
        {
            throw new System.NotImplementedException();
        }

        private void UpdateXref<TBase, TXref>(TXref presentXref, TXref xref)
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
        {
            throw new System.NotImplementedException();
        }
    }
}