using System;
using System.Collections.Generic;

namespace DotMaster.Core.Model.Impl
{
    public abstract class AbstractBaseObject<TKey, TBase, TXref> : IBaseObject<TKey, TBase, TXref>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        public virtual TKey ObjKey { get; set; }

        public virtual IList<TXref> Xrefs { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}
