using System;
using DotMaster.Core.Model;

namespace DotMaster.Core
{
    public interface IMasterDataBase
    {
        /// <summary>
        /// Найти перекрёстную ссылку для данного объекта из определённого источника
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="sourceKey">Ключ из источника</param>
        /// <param name="source">Источник</param>
        /// <returns>Найденная перекрёстная ссылка, либо null</returns>
        TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Сохранить базовый объект со всеми перекрёстными ссылками в базу
        /// Необходимо реализовать этот метод с каскадым сохранением перекрёстных ссылок!
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="baseObject">Базовый объект для сохранения</param>
        /// <returns>Сохранённый базовый объект</returns>
        void Save<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        void Update<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

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
        TBase QueryForBaseObject<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;
    }
}