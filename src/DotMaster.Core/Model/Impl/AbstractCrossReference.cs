using System;

namespace DotMaster.Core.Model.Impl
{
    public abstract class AbstractCrossReference<TKey, TBase, TXref> : ICrossReference<TKey, TBase, TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        public virtual TKey ObjKey { get; set; }
        public virtual TKey BaseObjKey { get; set; }

        public virtual string Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual TBase ObjectData { get; set; }
        public virtual TBase BaseObject { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}
