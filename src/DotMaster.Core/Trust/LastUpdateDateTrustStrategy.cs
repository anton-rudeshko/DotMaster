using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Trust
{
    public class LastUpdateDateTrustStrategy : ITrustStrategy
    {
        public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return 10; // todo
        }
    }
}
