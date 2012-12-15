using System;
using System.Collections.Generic;

namespace DotMaster.Core.Model.Impl
{
    public abstract class LongBaseObject<TBase, TXref> : IBaseObject<long, TBase, TXref>
        where TBase : LongBaseObject<TBase, TXref>
        where TXref : LongCrossReference<TBase, TXref>
    {
        public virtual long ObjKey { get; set; }

        public virtual IList<TXref> Xrefs { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}