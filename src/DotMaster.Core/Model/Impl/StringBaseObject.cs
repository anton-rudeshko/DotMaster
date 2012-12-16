namespace DotMaster.Core.Model.Impl
{
    public abstract class StringBaseObject<TBase, TXref> : AbstractBaseObject<string, TBase, TXref>
        where TBase : StringBaseObject<TBase, TXref>
        where TXref : StringCrossReference<TBase, TXref> {}
}
