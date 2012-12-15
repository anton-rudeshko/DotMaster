using System;
using DotMaster.Core.Model;
using DotMaster.Core.Trust;

namespace DotMaster.Tests.Trust
{
    internal class TestTrustStrategy : ITrustStrategy
    {
        public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref) 
            where TBase : class, IBaseObject<TKey, TBase, TXref> 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            throw new NotImplementedException();
        }
    }
}