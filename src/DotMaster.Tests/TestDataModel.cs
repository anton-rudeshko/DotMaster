using DotMaster.Core.Model.Impl;
using DotMaster.NHibernate.Mappings;

namespace DotMaster.Tests
{
    public class TestBO : IntBaseObject<TestBO, TestXref>
    {
        public virtual string MyProperty { get; set; }
        public virtual string MyProperty2 { get; set; }
    }

    public class TestXref : IntCrossReference<TestBO, TestXref> {}

    public class TestProvider : IntSourceDataProvider<TestBO, TestXref>
    {
        public TestProvider() : base("TestSource") {}
    }

    public class TestBOMap : IntBaseObjectMap<TestBO, TestXref>
    {
        public TestBOMap()
        {
            Map(x => x.MyProperty);
            Map(x => x.MyProperty2);
        }
    }

    public class TestXrefMap : IntXrefMap<TestBO, TestXref>
    {
        public TestXrefMap()
        {
            Component(x => x.ObjectData, m =>
                {
                    m.Map(x => x.MyProperty);
                    m.Map(x => x.MyProperty2);
                });
        }
    }
}
