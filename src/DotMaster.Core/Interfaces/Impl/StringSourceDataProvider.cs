using System;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class StringSourceDataProvider<TBase, TXref> : ISourceDataProvider<string, TBase, TXref>
        where TXref : StringCrossReference<TBase, TXref>
        where TBase : StringBaseObject<TBase, TXref>
    {
        public virtual event Action<TXref> OnData;
    }
}