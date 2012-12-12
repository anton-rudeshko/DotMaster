namespace DotMaster.Core.Interfaces
{
    public interface ITrustStrategy
    {
        int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;
    }
}