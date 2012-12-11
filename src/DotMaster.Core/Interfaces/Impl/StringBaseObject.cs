using System;
using System.Collections.Generic;

namespace DotMaster.Core.Interfaces.Impl
{
    public abstract class StringBaseObject<TBase, TXref> : IBaseObject<string, TBase, TXref>
        where TBase : StringBaseObject<TBase, TXref>
        where TXref : StringCrossReference<TBase, TXref>
    {
        public virtual string ObjKey { get; set; }

        public virtual IList<TXref> Xrefs { get; set; }

        public virtual DateTime LastUpdate { get; set; }
    }
}