using DotMaster.Core.Model;
using FluentNHibernate.Mapping;

namespace DotMaster.NHibernate.Mappings
{
    public class StringBaseObjectMap<TBase, TXref> : ClassMap<TBase>
        where TBase : class, IBaseObject<string, TBase, TXref>
        where TXref : class, ICrossReference<string, TBase, TXref>
    {

    }
}