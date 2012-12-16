using DotMaster.Core.Model;

namespace DotMaster.NHibernate.Mappings
{
    public class IntBaseObjectMap<TBase, TXref> : BaseObjectMap<int, TBase, TXref>
        where TBase : class, IBaseObject<int, TBase, TXref>
        where TXref : class, ICrossReference<int, TBase, TXref> {}
}