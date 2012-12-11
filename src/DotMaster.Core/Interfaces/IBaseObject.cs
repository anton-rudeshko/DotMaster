using System;
using System.Collections.Generic;

namespace DotMaster.Core.Interfaces
{
    public interface IBaseObject<TBase, TXref> : IEntity
        where TBase : class, IBaseObject<TBase, TXref>
        where TXref : class, ICrossReference<TBase, TXref>
    {
        IList<TXref> Xrefs { get; set; }

        DateTime LastUpdate { get; set; }
    }
}