using System;
using System.Collections.Generic;
using DotMaster.Core.Interfaces;
using FluentNHibernate.Mapping;

namespace DotMaster.Tests
{
    public class TestBO : IBaseObject<long, TestBO, TestXref>
    {
        public virtual long ObjKey { get; set; }
        public virtual IList<TestXref> Xrefs { get; set; }
        public virtual DateTime LastUpdate { get; set; }

        public virtual string MyProperty { get; set; }
    }

    public class TestXref : ICrossReference<long, TestBO, TestXref>
    {
        public virtual long ObjKey { get; set; }
        public virtual long BaseObjKey { get; set; }

        public virtual string Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual TestBO ObjectData { get; set; }

        public virtual TestBO BaseObject { get; set; }
        public virtual DateTime UpdateDate { get; set; }
    }

    public class TestProvider : ISourceDataProvider<long, TestBO, TestXref>
    {
        public virtual event Action<TestXref> OnData;
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