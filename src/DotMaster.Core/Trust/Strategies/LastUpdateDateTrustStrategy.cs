using DotMaster.Core.Model;

namespace DotMaster.Core.Trust.Strategies
{
    public class LastUpdateDateTrustStrategy : ITrustStrategy
    {
        public int GetXrefScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return 10; // todo
        }

        public object GetWinValue<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            throw new System.NotImplementedException();
        }
    }
}
