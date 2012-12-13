using DotMaster.Core.Abstract;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class LongSourceDataProvider<TBase, TXref> : BaseSourceDataProvider<long, TBase, TXref>
        where TXref : LongCrossReference<TBase, TXref>
        where TBase : LongBaseObject<TBase, TXref>
    {
        protected LongSourceDataProvider(string source) : base(source)
        {
        }
    }
}
