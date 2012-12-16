using DotMaster.Core.Model;

namespace DotMaster.NHibernate.Mappings
{
    public class LongXrefMap<TBase, TXref> : XrefMap<long, TBase, TXref>
        where TBase : class, IBaseObject<long, TBase, TXref>
        where TXref : class, ICrossReference<long, TBase, TXref> {}
}
