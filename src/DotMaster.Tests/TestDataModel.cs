using System.Collections.Generic;
using DotMaster.Core.Interfaces;
using FluentNHibernate.Mapping;

namespace DotMaster.Tests
{
    public class TestBO : IBaseObject
    {
        public virtual string ObjKey { get; set; }
        public virtual IList<ICrossReference> Xrefs { get; set; }

        public virtual string MyProperty { get; set; }
    }

    public class TestXref : ICrossReference
    {
        public virtual string ObjKey { get; set; }
        public virtual string BaseObjKey { get; set; }

        public virtual ISource Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual IBaseObject Object { get; set; }

        public virtual int MySecondProperty { get; set; }
        public virtual IBaseObject BaseObject { get; set; }
    }

    public class TestBOMap : ClassMap<TestBO>
    {
        public TestBOMap()
        {
            Id(x => x.ObjKey);
            Map(x => x.MyProperty);
            HasMany<TestXref>(x => x.Xrefs);
        }
    }

    public class TestXrefMap : ClassMap<TestXref>
    {
        public TestXrefMap()
        {
            Id(x => x.ObjKey);
            Map(x => x.SourceKey);
            Map(x => x.MySecondProperty);
            References<TestBO>(x => x.BaseObject);
        }
    }
}