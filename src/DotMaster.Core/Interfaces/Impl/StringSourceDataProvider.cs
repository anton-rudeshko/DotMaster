using DotMaster.Core.Abstract;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class StringSourceDataProvider<TBase, TXref> : BaseSourceDataProvider<string, TBase, TXref>
        where TXref : StringCrossReference<TBase, TXref>
        where TBase : StringBaseObject<TBase, TXref>
    {
        protected StringSourceDataProvider(string source) : base(source)
        {
        }
    }
}
