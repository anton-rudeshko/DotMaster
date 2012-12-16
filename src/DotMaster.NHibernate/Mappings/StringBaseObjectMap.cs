using DotMaster.Core.Model;

namespace DotMaster.NHibernate.Mappings
{
    public class StringBaseObjectMap<TBase, TXref> : BaseObjectMap<string, TBase, TXref>
        where TBase : class, IBaseObject<string, TBase, TXref>
        where TXref : class, ICrossReference<string, TBase, TXref> {}
}
