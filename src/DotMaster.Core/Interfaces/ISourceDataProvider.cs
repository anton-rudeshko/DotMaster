using System;

namespace DotMaster.Core.Interfaces
{
    public interface ISourceDataProvider<TKey, TBase, out TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
    {
        event Action<TXref> OnData;
    }
}