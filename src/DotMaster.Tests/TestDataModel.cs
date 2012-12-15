using DotMaster.Core.Model;
using DotMaster.Core.Model.Impl;
using FluentNHibernate.Mapping;

namespace DotMaster.Tests
{
    public class TestBO : LongBaseObject<TestBO, TestXref>
    {
        public virtual string MyProperty { get; set; }

        public virtual string MyProperty2 { get; set; }
    }

    public class TestXref : LongCrossReference<TestBO, TestXref>
    {

    }

    public class TestProvider : LongSourceDataProvider<TestBO, TestXref>
    {
        public TestProvider() : base("TestSource")
        {
        }
    }

    public class BaseObjectMap<TKey, TBase, TXref> : ClassMap<TBase>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        public BaseObjectMap()
        {
            Id(x => x.ObjKey).Not.Nullable();

            Map(x => x.LastUpdate).Not.Nullable();

            HasMany(x => x.Xrefs).KeyColumn("BaseObjKey");
        }
    }

    public class XrefMap<TKey, TBase, TXref> : ClassMap<TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        public XrefMap()
        {
            Id(x => x.ObjKey).Not.Nullable();

            Map(x => x.LastUpdate).Not.Nullable();
            Map(x => x.SourceKey).Not.Nullable();

            References(x => x.BaseObject).Column("BaseObjKey").Not.Nullable();
        }
    }

    public class TestBOMap : BaseObjectMap<long, TestBO, TestXref>
    {
        public TestBOMap()
        {
            Map(x => x.MyProperty);
            Map(x => x.MyProperty2);
        }
    }

    public class TestXrefMap : XrefMap<long, TestBO, TestXref>
    {
        public TestXrefMap()
        {

        }
    }
}