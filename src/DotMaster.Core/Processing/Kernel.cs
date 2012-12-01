using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Processing
{
    /// <summary>
    /// Главный класс!
    /// Хотя что он делает, ещё пока не ясно
    /// </summary>
    public class Kernel
    {
        public Kernel(IMasterDataBase masterDB)
        {
            MasterDB = masterDB;
        }

        public IMasterDataBase MasterDB { get; set; }

        public void RegisterDataProvider(ISourceDataProvider dataProvider)
        {
            dataProvider.OnData += Process;
        }

        private void Process(ICrossReference xref)
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

        private void CreateBaseObjectFromXref(ICrossReference xref)
        {
            MasterDB.CreateBaseObjectFrom(xref);
        }

        private void AddXrefToBaseObject(IBaseObject baseObject, ICrossReference xref)
        {
            MasterDB.AppendXrefTo(baseObject, xref);
        }

        public void StartMatchAndMerge()
        {
        }
    }
}