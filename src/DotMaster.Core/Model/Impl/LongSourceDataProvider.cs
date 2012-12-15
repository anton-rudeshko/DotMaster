namespace DotMaster.Core.Model.Impl
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
