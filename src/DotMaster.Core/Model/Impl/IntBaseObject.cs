namespace DotMaster.Core.Model.Impl
{
    public abstract class IntBaseObject<TBase, TXref> : AbstractBaseObject<int, TBase, TXref>
        where TBase : IntBaseObject<TBase, TXref>
        where TXref : IntCrossReference<TBase, TXref> {}
}
