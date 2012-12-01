using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Processing
{
    /// <summary>
    /// Главный класс!
    /// Хотя что он делает, ещё пока не ясно
    /// </summary>
    public class Kernel<TXref, TBase>
        where TXref : class, ICrossReference<TBase>
        where TBase : IBaseObject
    {
        public Kernel(IMasterDataBase<TXref, TBase> masterDB)
        {
            MasterDB = masterDB;
        }

        public IMasterDataBase<TXref, TBase> MasterDB { get; set; }

        public void RegisterDataProvider(ISourceDataProvider<TXref, TBase> dataProvider)
        {
            dataProvider.OnData += Process;
        }

        private void Process(TXref xref)
        {
            var baseObject = MasterDB.BaseObjectFor(xref);
            if (baseObject == null)
            {
                CreateBaseObjectFromXref(xref);
            }
            else
            {
                AddXrefToBaseObject(baseObject, xref);
            }
        }

        private void CreateBaseObjectFromXref(TXref xref)
        {
            MasterDB.CreateBaseObjectFrom(xref);
        }

        private void AddXrefToBaseObject(TBase baseObject, TXref xref)
        {
            MasterDB.AppendXrefTo(baseObject, xref);
        }

        public void StartMatchAndMerge()
        {
        }
    }
}