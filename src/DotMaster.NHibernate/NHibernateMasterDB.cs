using System;
using System.Collections.Generic;
using System.Linq;
using DotMaster.Core.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace DotMaster.NHibernate
{
    public class NHibernateMasterDB // : IMasterDataBase
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

        public TBase BaseObjectFor<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            throw new NotImplementedException();
        }

        public void CreateBaseObjectFrom<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            throw new NotImplementedException();
        }

        public void AppendXrefTo<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            throw new NotImplementedException();
        }

        public TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return CurrentSession.Query<TXref>().FirstOrDefault(x => x.SourceKey == sourceKey && x.Source == source);
        }

        public IEnumerable<TXref> QueryForXrefs<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return CurrentSession.Query<TXref>().Where(x => x.SourceKey == sourceKey && x.Source == source);
        }
    }
}