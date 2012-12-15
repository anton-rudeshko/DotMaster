using DotMaster.Core.Model;
using FluentNHibernate.Mapping;

namespace DotMaster.NHibernate.Mappings
{
    public class StringXrefMap<TBase, TXref> : ClassMap<TXref>
        where TBase : class, IBaseObject<string, TBase, TXref>
        where TXref : class, ICrossReference<string, TBase, TXref>
    {

    }
}