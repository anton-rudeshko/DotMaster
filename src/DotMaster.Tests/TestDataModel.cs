using System;
using System.Collections.Generic;
using DotMaster.Core.Interfaces;
using FluentNHibernate.Mapping;

namespace DotMaster.Tests
{
    public class TestBO : IBaseObject<TestBO, TestXref>
    {
        public virtual string ObjKey { get; set; }
        public virtual IList<TestXref> Xrefs { get; set; }

        public virtual string MyProperty { get; set; }
    }

    public class TestXref : ICrossReference<TestBO, TestXref>
    {
        public virtual string ObjKey { get; set; }
        public virtual string BaseObjKey { get; set; }

        public virtual string Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual TestBO Object { get; set; }

        public virtual int MySecondProperty { get; set; }
        public virtual TestBO BaseObject { get; set; }
    }

    public class TestProvider : ISourceDataProvider<TestBO, TestXref>
    {
        public event Action<TestXref> OnData;
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
            Map(x => x.MySecondProperty);
            References(x => x.BaseObject);
        }
    }
}