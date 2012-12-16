using System;
using System.Collections.Generic;

namespace DotMaster.Core.Model.Impl
{
    public abstract class IntBaseObject<TBase, TXref> : IBaseObject<int, TBase, TXref>
        where TBase : IntBaseObject<TBase, TXref>
        where TXref : IntCrossReference<TBase, TXref>
    {
        public virtual int ObjKey { get; set; }

        public virtual IList<TXref> Xrefs { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}