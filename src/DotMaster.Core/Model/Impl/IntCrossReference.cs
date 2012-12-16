using System;

namespace DotMaster.Core.Model.Impl
{
    public abstract class IntCrossReference<TBase, TXref> : ICrossReference<int, TBase, TXref>
        where TBase : IntBaseObject<TBase, TXref>
        where TXref : IntCrossReference<TBase, TXref>
    {
        public virtual int ObjKey { get; set; }
        public virtual int BaseObjKey { get; set; }

        public virtual string Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual TBase ObjectData { get; set; }
        public virtual TBase BaseObject { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}