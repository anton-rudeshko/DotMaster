using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Processing
{
    public class Kernel
    {
        public Kernel(IMasterDataBase masterDB)
        {
            MasterDB = masterDB;
        }

        public IMasterDataBase MasterDB { get; set; }

        public void RegisterDataProvider<TBase, TXref>(ISourceDataProvider<TBase, TXref> dataProvider)
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
        {
            dataProvider.OnData += Process<TBase, TXref>;
        }

        private void Process<TBase, TXref>(TXref xref)
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
        {
            TXref presentXref;
            if (TryGetXref<TBase, TXref>(xref, out presentXref))
            {
                UpdateXref<TBase, TXref>(presentXref, xref);
                Save<TBase, TXref>(presentXref);
            }
            else
            {
                CreateBaseObjectFromXref<TBase, TXref>(xref);
            }
        }

        private bool TryGetXref<TBase, TXref>(TXref xref, out TXref presentXref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>
        {
            presentXref = MasterDB.QueryForXref<TBase, TXref>(xref.SourceKey, xref.Source);
            return presentXref == null;
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

        private void CreateBaseObjectFromXref<TBase, TXref>(TXref xref)
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
        {
            throw new System.NotImplementedException();
        }

        private void AddXrefToBaseObject<TBase, TXref>(TBase baseObject, TXref xref)
            where TXref : class, ICrossReference<TBase, TXref>
            where TBase : class, IBaseObject<TBase, TXref>
        {
            throw new System.NotImplementedException();
        }

        public void StartMatchAndMerge()
        {
            throw new System.NotImplementedException();
        }
    }
}