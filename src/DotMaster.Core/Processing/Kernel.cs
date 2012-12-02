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

        public void RegisterDataProvider<TBase, TXref>(ISourceDataProvider<TXref> dataProvider) 
            where TXref : class, ICrossReference 
            where TBase : class, IBaseObject
        {
            dataProvider.OnData += Process<TBase, TXref>;
        }

        private void Process<TBase, TXref>(TXref xref) 
            where TXref : class, ICrossReference 
            where TBase : class, IBaseObject
        {
            var baseObject = MasterDB.BaseObjectFor<TBase, TXref>(xref);
            if (baseObject == null)
            {
//                CreateBaseObjectFromXref(xref);
            }
            else
            {
//                AddXrefToBaseObject(baseObject, xref);
            }
        }

        private void CreateBaseObjectFromXref(ICrossReference xref)
        {
//            MasterDB.CreateBaseObjectFrom(xref);
        }

        private void AddXrefToBaseObject(IBaseObject baseObject, ICrossReference xref)
        {
//            MasterDB.AppendXrefTo(baseObject, xref);
        }

        public void StartMatchAndMerge()
        {
        }
    }
}