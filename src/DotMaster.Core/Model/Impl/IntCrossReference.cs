namespace DotMaster.Core.Model.Impl
{
    public abstract class IntCrossReference<TBase, TXref> : AbstractCrossReference<int, TBase, TXref>
        where TBase : IntBaseObject<TBase, TXref>
        where TXref : IntCrossReference<TBase, TXref> {}
}
