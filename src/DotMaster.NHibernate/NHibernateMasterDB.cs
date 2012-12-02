using System;
using System.Collections.Generic;
using System.Linq;
using DotMaster.Core.Interfaces;
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

        public TXref XrefFor<TXref>(string sourceKey) where TXref : class, ICrossReference
        {
            return CurrentSession.Query<TXref>().FirstOrDefault(x => x.SourceKey == sourceKey);
        }

        public IEnumerable<TXref> XrefsFor<TXref>(string sourceKey) where TXref : class, ICrossReference
        {
            return CurrentSession.Query<TXref>().Where(x => x.SourceKey == sourceKey);
        }

        public TBase BaseObjectFor<TBase, TXref>(TXref xref)
            where TXref : class, ICrossReference
            where TBase : class, IBaseObject
        {
            CheckXref(xref);

            var presentXref = XrefFor<TXref>(xref.SourceKey);
            return presentXref == null ? null : BaseObjectFor<TBase>(presentXref.BaseObjKey);
        }

        public void CreateBaseObjectFrom<TXref>(TXref xref) where TXref : class, ICrossReference
        {
            throw new NotImplementedException();
        }

        public TBase BaseObjectFor<TBase>(string baseObjKey)
            where TBase : class, IBaseObject
        {
            return CurrentSession.Query<TBase>().FirstOrDefault(x => x.ObjKey == baseObjKey);
        }

        public void CreateBaseObjectFrom<TBase, TXref>(TXref xref)
            where TXref : class, ICrossReference
            where TBase : class, IBaseObject
        {
            CheckXref(xref);

            var baseObject = BaseObjectFor<TBase>(xref.SourceKey);
            if (baseObject != null)
            {
                throw new ArgumentException("Base object for current xref already present", "xref");
            }
            SaveBaseObject<TBase>(null);
        }

        private void SaveBaseObject<TBase>(TBase baseObject) 
            where TBase : class, IBaseObject
        {
            CurrentSession.Save(baseObject);
        }

        public void AppendXrefTo<TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject
            where TXref : class, ICrossReference
        {
            throw new NotImplementedException();
        }

        private static void CheckXref<TXref>(TXref xref)
            where TXref : class, ICrossReference
        {
            if (xref == null)
            {
                throw new ArgumentNullException("xref");
            }
            if (string.IsNullOrWhiteSpace(xref.SourceKey))
            {
                throw new ArgumentException("Could not find base object for xref without source key", "xref");
            }
        }
    }
}