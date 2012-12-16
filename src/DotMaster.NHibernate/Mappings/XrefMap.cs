using DotMaster.Core.Model;
using DotMaster.Core.Utils;
using FluentNHibernate.Mapping;

namespace DotMaster.NHibernate.Mappings
{
    public class XrefMap<TKey, TBase, TXref> : ClassMap<TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        public XrefMap()
        {
            var baseObjKey = ReflectionUtils.NameOf((TXref x) => x.BaseObjKey);
            Id(x => x.ObjKey).Not.Nullable();

            Map(x => x.LastUpdate).Not.Nullable();
            Map(x => x.Source).Not.Nullable();
            Map(x => x.SourceKey).Not.Nullable();
            Map(x => x.BaseObjKey).Not.Nullable().Formula(baseObjKey);

            References(x => x.BaseObject, baseObjKey).Not.Nullable();
        }
    }
}
