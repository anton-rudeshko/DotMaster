namespace DotMaster.Core.Model.Impl
{
    public abstract class LongBaseObject<TBase, TXref> : AbstractBaseObject<long, TBase, TXref>
        where TBase : LongBaseObject<TBase, TXref>
        where TXref : LongCrossReference<TBase, TXref> {}
}
