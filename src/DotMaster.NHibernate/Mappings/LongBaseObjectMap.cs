using DotMaster.Core.Model;
using FluentNHibernate.Mapping;

namespace DotMaster.NHibernate.Mappings
{
    public class LongBaseObjectMap<TBase, TXref> : ClassMap<TBase>
        where TBase : class, IBaseObject<long, TBase, TXref>
        where TXref : class, ICrossReference<long, TBase, TXref>
    {

    }
}