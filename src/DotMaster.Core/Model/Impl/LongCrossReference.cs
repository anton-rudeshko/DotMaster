namespace DotMaster.Core.Model.Impl
{
    public abstract class LongCrossReference<TBase, TXref> : AbstractCrossReference<long, TBase, TXref>
        where TBase : LongBaseObject<TBase, TXref>
        where TXref : LongCrossReference<TBase, TXref> {}
}
