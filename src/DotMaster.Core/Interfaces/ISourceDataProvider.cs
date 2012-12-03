using System;

namespace DotMaster.Core.Interfaces
{
    public interface ISourceDataProvider<TBase, out TXref>
        where TXref : class, ICrossReference<TBase, TXref>
        where TBase : class, IBaseObject<TBase, TXref>
    {
        event Action<TXref> OnData;
    }
}