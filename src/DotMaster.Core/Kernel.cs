using System;
using System.Collections.Generic;
using System.Diagnostics;
using DotMaster.Core.Model;
using DotMaster.Core.Trust;

namespace DotMaster.Core
{
    /// <summary>
    /// Вычислительное ядро модуля DotMaster.
    /// Предоставляет интерфейс для обработки основных данных.
    /// </summary>
    public class Kernel
    {
        private readonly TrustProcessor _trustProcessor;
        public IMasterDataBase MasterDB { get; set; }

        public Kernel(IMasterDataBase masterDB = null) : this(masterDB, new TrustProcessor()) {}

        public Kernel(IMasterDataBase masterDB, TrustProcessor trustProcessor)
        {
            if (trustProcessor == null)
            {
                throw new ArgumentNullException("trustProcessor");
            }

            MasterDB = masterDB;
            _trustProcessor = trustProcessor;
        }

        /// <summary>
        /// Зарегистрировать нового поставщика данных и подписаться на него обновления
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="dataProvider">Поставщик данных</param>
        public void RegisterDataProvider<TKey, TBase, TXref>(ISourceDataProvider<TKey, TBase, TXref> dataProvider)
            where TBase : class, IBaseObject<TKey, TBase, TXref>, new() 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }

            dataProvider.OnData += Process<TKey, TBase, TXref>;
        }

        /// <summary>
        /// Обработать новое обновление в виде перекрёстной ссылки.
        /// Перекрёстная ссылка будет добавлена в базовый объект, поля которого
        /// будут обновлены в соответствии с переданной перекрёстной ссылкой.
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="xref">Перекрёстная ссылка, содержащая обновления</param>
        public void Process<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>, new()
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (xref == null)
            {
                throw new ArgumentNullException("xref");
            }
            if (string.IsNullOrWhiteSpace(xref.Source))
            {
                throw new ArgumentException(I18n.XrefSourceIsEmpty, "xref");
            }
            if (string.IsNullOrWhiteSpace(xref.SourceKey))
            {
                throw new ArgumentException(I18n.XrefSourceKeyIsEmpty, "xref");
            }

            var presentXref = MasterDB.QueryForXref<TKey, TBase, TXref>(xref.SourceKey, xref.Source);
            TBase baseObject;
            if (presentXref != null)
            {
                // Обновить уже существующую перекрёстную ссылку из данного источника
                Debug.WriteLine("Found present xref " + presentXref.BaseObjKey);
                presentXref.ObjectData = xref.ObjectData;
                presentXref.LastUpdate = xref.LastUpdate;
                baseObject = presentXref.BaseObject;
            }
            else
            {
                // Создать нвоый базовый объект на основе пришедшего обновления
                Debug.WriteLine("No present xref found, creating new base object");
                baseObject = new TBase { Xrefs = new List<TXref> { xref } };
                xref.BaseObject = baseObject;
            }

            RecalculateTrust<TKey, TBase, TXref>(baseObject);
        }

        /// <summary>
        /// Пересчитать доверительные правила для данного базового объекта и обновить его в базе данных
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="baseObject">Базовый объект</param>
        public void RecalculateTrust<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            if (baseObject == null)
            {
                throw new ArgumentNullException("baseObject");
            }

            _trustProcessor.CalculateTrust<TKey, TBase, TXref>(baseObject);
            MasterDB.Update<TKey, TBase, TXref>(baseObject);
        }
    }
}
