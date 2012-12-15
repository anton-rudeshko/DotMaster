using System;
using DotMaster.Core.Model;

namespace DotMaster.Core
{
    public interface ISourceDataProvider<TKey, TBase, out TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
    {
        event Action<TXref> OnData;
    }
}