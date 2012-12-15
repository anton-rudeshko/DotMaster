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
            Id(x => x.ObjKey).Not.Nullable();

            Map(x => x.LastUpdate).Not.Nullable();
            Map(x => x.Source).Not.Nullable();
            Map(x => x.SourceKey).Not.Nullable();

            References(x => x.BaseObject).Column(ReflectionUtils.NameOf((TXref x) => x.BaseObjKey)).Not.Nullable();
        }
    }
}
