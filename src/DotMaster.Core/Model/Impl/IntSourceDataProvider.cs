namespace DotMaster.Core.Model.Impl
{
    public abstract class IntSourceDataProvider<TBase, TXref> : BaseSourceDataProvider<int, TBase, TXref>
        where TXref : IntCrossReference<TBase, TXref>
        where TBase : IntBaseObject<TBase, TXref>
    {
        protected IntSourceDataProvider(string source) : base(source) {}
    }
}