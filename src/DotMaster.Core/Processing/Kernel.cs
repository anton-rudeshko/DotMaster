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
            var presentXref = MasterDB.QueryForXref<TBase, TXref>(xref.SourceKey, xref.Source);
            if (presentXref == null)
            {
                CreateBaseObjectFromXref<TBase, TXref>(xref);
            }
            else
            {
                UpdateXref<TBase, TXref>(presentXref, xref);
                Save<TBase, TXref>(presentXref);
            }
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