using System;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class LongCrossReference<TBase, TXref> : ICrossReference<long, TBase, TXref>
        where TBase : LongBaseObject<TBase, TXref>
        where TXref : LongCrossReference<TBase, TXref>
    {
        public virtual long ObjKey { get; set; }
        public virtual long BaseObjKey { get; set; }

        public virtual string Source { get; set; }
        public virtual string SourceKey { get; set; }

        public virtual TBase ObjectData { get; set; }
        public virtual TBase BaseObject { get; set; }

        public virtual DateTime UpdateDate { get; set; }
    }
}