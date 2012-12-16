namespace DotMaster.Core.Model.Impl
{
    public abstract class StringCrossReference<TBase, TXref> : AbstractCrossReference<string, TBase, TXref>
        where TBase : StringBaseObject<TBase, TXref>
        where TXref : StringCrossReference<TBase, TXref> {}
}
