using System;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class LongSourceDataProvider<TBase, TXref> : ISourceDataProvider<long, TBase, TXref>
        where TXref : LongCrossReference<TBase, TXref>
        where TBase : LongBaseObject<TBase, TXref>
    {
        public virtual event Action<TXref> OnData;
    }
}