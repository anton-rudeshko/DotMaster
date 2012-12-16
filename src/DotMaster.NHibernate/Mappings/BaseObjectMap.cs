using DotMaster.Core.Model;
using DotMaster.Core.Utils;
using FluentNHibernate.Mapping;

namespace DotMaster.NHibernate.Mappings
{
    public class BaseObjectMap<TKey, TBase, TXref> : ClassMap<TBase>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        public BaseObjectMap()
        {
            Id(x => x.ObjKey).Not.Nullable();

            Map(x => x.LastUpdate).Not.Nullable();

            HasMany(x => x.Xrefs).KeyColumn(ReflectionUtils.NameOf((TXref x) => x.BaseObjKey)).Cascade.All();
        }
    }
}