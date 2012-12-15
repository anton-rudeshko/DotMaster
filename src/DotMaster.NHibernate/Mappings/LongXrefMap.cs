using DotMaster.Core.Model;
using FluentNHibernate.Mapping;

namespace DotMaster.NHibernate.Mappings
{
    public class LongXrefMap<TBase, TXref> : ClassMap<TXref>
        where TBase : class, IBaseObject<long, TBase, TXref>
        where TXref : class, ICrossReference<long, TBase, TXref>
    {

    }
}