using System.Collections.Generic;

namespace DotMaster.Core.Interfaces
{
    public interface IBaseObject : IEntity
    {
        IList<ICrossReference> Xrefs { get; set; }
    }
}