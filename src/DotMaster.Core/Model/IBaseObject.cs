using System;
using System.Collections.Generic;

namespace DotMaster.Core.Model
{
    public interface IBaseObject<TKey, TBase, TXref> : IEntity<TKey>
        where TBase : class, IBaseObject<TKey, TBase, TXref>
        where TXref : class, ICrossReference<TKey, TBase, TXref>
    {
        IList<TXref> Xrefs { get; set; }

        DateTime LastUpdate { get; set; }
    }
}