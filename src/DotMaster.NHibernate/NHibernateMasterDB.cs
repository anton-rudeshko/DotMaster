using System;
using System.Collections.Generic;
using System.Linq;
using DotMaster.Core;
using DotMaster.Core.Model;
using NHibernate;
using NHibernate.Linq;

namespace DotMaster.NHibernate
{
    public class NHibernateMasterDB : IMasterDataBase
    {
        private readonly ISessionFactory _sessionFactory;

        private ISession CurrentSession
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        public NHibernateMasterDB(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return CurrentSession.Query<TXref>().SingleOrDefault(x => x.SourceKey == sourceKey && x.Source == source);
        }

        public TBase Save<TKey, TBase, TXref>(TBase baseObject) 
            where TBase : class, IBaseObject<TKey, TBase, TXref> 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return (TBase) CurrentSession.Save(baseObject);
        }
    }
}