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

    public class TestBOMap : ClassMap<TestBO>
    {
        public TestBOMap()
        {
            Id(x => x.ObjKey);
            Map(x => x.MyProperty);
            HasMany(x => x.Xrefs);
        }
    }

    public class TestXrefMap : ClassMap<TestXref>
    {
        public TestXrefMap()
        {
            Id(x => x.ObjKey);
            Map(x => x.SourceKey);
            References(x => x.BaseObject);
        }
    }
}