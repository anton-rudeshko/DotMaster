using System;
using System.Collections.Generic;
using DotMaster.Core.Model;

namespace DotMaster.Core.Match
{
    public class Matcher
    {
        private readonly IMasterDataBase _masterDataBase;

        public Matcher(IMasterDataBase masterDataBase)
        {
            if (masterDataBase == null)
            {
                throw new ArgumentNullException("masterDataBase");
            }

            // todo: read match rules
            _masterDataBase = masterDataBase;
        }

        public void MatchAll<TKey, TBase, TXref>()
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            var forMatch = _masterDataBase.BaseObjectsForMatch<TKey, TBase, TXref>();
            foreach (var baseObject in forMatch) {}
        }

        public void Match<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref> {}

        public void Match<TKey, TBase, TXref>(IEnumerable<TBase> baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref> {}

        public bool Match<TKey, TBase, TXref>(TBase forMatch, TBase golden)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (forMatch == null)
            {
                throw new ArgumentNullException("forMatch");
            }
            if (golden == null)
            {
                throw new ArgumentNullException("golden");
            }
            
            // todo: apply match rules

            return false;
        }
    }
}
