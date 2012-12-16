using System;
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

        public void Save<TKey, TBase, TXref>(TBase baseObject) 
            where TBase : class, IBaseObject<TKey, TBase, TXref> 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            using (var tx = CurrentSession.BeginTransaction())
            {
                CurrentSession.SaveOrUpdate(baseObject);
                tx.Commit();
            }
        }

        public void Update<TKey, TBase, TXref>(TBase baseObject) 
            where TBase : class, IBaseObject<TKey, TBase, TXref> 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            using (var tx = CurrentSession.BeginTransaction())
            {
                CurrentSession.Update(baseObject);
                tx.Commit();
            }
        }
    }
}