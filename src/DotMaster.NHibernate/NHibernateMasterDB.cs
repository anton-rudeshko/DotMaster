using System;
using System.Linq;
using DotMaster.Core;
using DotMaster.Core.Model;
using NHibernate;
using NHibernate.Linq;

namespace DotMaster.NHibernate
{
    /// <summary>
    /// Реализация основной базы данных для NHibernate
    /// </summary>
    public class NHibernateMasterDB : IMasterDataBase
    {
        private readonly ISessionFactory _sessionFactory;

        private ISession CurrentSession
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        public NHibernateMasterDB(ISessionFactory sessionFactory)
        {
            if (sessionFactory == null)
            {
                throw new ArgumentNullException("sessionFactory");
            }

            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Найти перекрёстную ссылку для данного объекта из определённого источника
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="sourceKey">Ключ из источника</param>
        /// <param name="source">Источник</param>
        /// <returns>Найденная перекрёстная ссылка, либо null</returns>
        public TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (sourceKey == null)
            {
                throw new ArgumentNullException("sourceKey");
            }
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return CurrentSession.Query<TXref>().SingleOrDefault(x => x.SourceKey == sourceKey && x.Source == source);
        }

        /// <summary>
        /// Сохранить базовый объект со всеми перекрёстными ссылками в базу
        /// Необходимо реализовать этот метод с каскадым сохранением перекрёстных ссылок!
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="baseObject">Базовый объект для сохранения</param>
        /// <returns>Сохранённый базовый объект</returns>
        public void Save<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            if (baseObject.Xrefs == null || baseObject.Xrefs.Count == 0)
            {
                throw new InvalidOperationException("You can't save base object without any xrefs");
            }

            InTransaction(() => CurrentSession.SaveOrUpdate(baseObject));
        }

        public void Update<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            InTransaction(() => CurrentSession.Update(baseObject));
        }

        /// <summary>
        /// Получить базовый объект для данной перекрёстной ссылки
        /// Для случая с ленивой подгрузкой можно не делать никаких запросов в базу, просто 
        /// вернуть базовый объект из переданной перекрёстной ссылки
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="xref">Перекрёстная ссылка на основе которой надо извлечь базовый объект</param>
        /// <returns>Базовый объект для данной перекрёстной ссылки</returns>
        /// <exception cref="InvalidOperationException">Если по данной перекрёстной
        /// ссылке ничего не найдено, это нарушение целостности данных</exception>
        public TBase QueryForBaseObject<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (xref == null)
            {
                throw new ArgumentNullException("xref");
            }

            // check if base object is already loaded (lazy)
            if (xref.BaseObject != null)
            {
                return xref.BaseObject;
            }

            // trying to be fault tolerant: searching BO in DB
            if (Equals(xref.BaseObjKey, default(TKey)))
            {
                throw new InvalidOperationException("There is no base object key in this xref, so base object can't be found");
            }
            
            xref.BaseObject = CurrentSession.Query<TBase>().SingleOrDefault(x => Equals(x.ObjKey, xref.BaseObjKey));
            if (xref.BaseObject == null)
            {
                throw new InvalidOperationException("There is base object for this xref, check your data consistency");
            }

            return xref.BaseObject;
        }

        private void InTransaction(Action action)
        {
            using (var tx = CurrentSession.BeginTransaction())
            {
                try
                {
                    action();
                    tx.Commit();
                }
                catch (Exception)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}