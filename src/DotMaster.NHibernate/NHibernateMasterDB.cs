using System;
using DotMaster.Core.Interfaces;

namespace DotMaster.NHibernate
{
    public class NHibernateMasterDB : IMasterDataBase
    {
        public IBaseObject BaseObjectFor(ICrossReference xref)
        {
            throw new NotImplementedException();
        }

        public void CreateBaseObjectFrom(ICrossReference xref)
        {
            throw new NotImplementedException();
        }

        public void AppendXrefTo(IBaseObject baseObject, ICrossReference xref)
        {
            throw new NotImplementedException();
        }
    }
}