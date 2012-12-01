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
        }

        private void AddXrefToBaseObject(TBase baseObject, TXref xref)
        {
        }

        public void StartMatchAndMerge()
        {
        }
    }

    public interface IMasterDataBase<TXref, TBase> where TXref : ICrossReference<TBase> where TBase : IBaseObject
    {
        TBase BaseObjectFor(TXref xref);
    }
}