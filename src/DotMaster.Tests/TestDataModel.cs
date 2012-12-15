using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;

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

    public class TestBOMap : LongBaseObjectMap<TestBO, TestXref>
    {
        public TestBOMap()
        {
            Map(x => x.MyProperty);
            Map(x => x.MyProperty2);
        }
    }

    public class TestXrefMap : LongXrefMap<TestBO, TestXref>
    {
        public TestXrefMap()
        {
            Map(x => x.ObjectData.MyProperty);
            Map(x => x.ObjectData.MyProperty2);
        }
    }
}