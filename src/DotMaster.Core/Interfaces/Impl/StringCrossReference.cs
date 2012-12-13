using System;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class StringCrossReference<TBase, TXref> : ICrossReference<string, TBase, TXref>
        where TBase : StringBaseObject<TBase, TXref>
        where TXref : StringCrossReference<TBase, TXref>
    {
        public virtual string ObjKey { get; set; }
        public virtual string BaseObjKey { get; set; }

        public virtual string Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual TBase ObjectData { get; set; }
        public virtual TBase BaseObject { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}