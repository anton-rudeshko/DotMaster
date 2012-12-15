using System.Collections.Generic;
using DotMaster.Core.Model;

namespace DotMaster.Core
{
    public interface IMasterDataBase
    {
        /// <summary>
        /// Найти base object для переданного xref.
        /// Xref должен иметь SourceKey, по нему будет производится поиск
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="xref">Перекрёстная ссылка</param>
        /// <returns>BO если найден, null иначе</returns>
        TBase BaseObjectFor<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Создать новый BO на основе переданного xref
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="xref">Перекрёстная ссылка</param>
        void CreateBaseObjectFrom<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Добавить xref в данный BO, пересчитать доверительные правила
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="baseObject">Базовый объект</param>
        /// <param name="xref">Перекрёстная ссылка</param>
        void AppendXrefTo<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        IEnumerable<TXref> QueryForXrefs<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Сохранить базовый объект со всеми перекрёстными ссылками в базу
        /// </summary>
        /// <typeparam name="TKey">Тип ключа</typeparam>
        /// <typeparam name="TBase">Тип базового объекта</typeparam>
        /// <typeparam name="TXref">Тип перекрёстной ссылки</typeparam>
        /// <param name="baseObject">Базовый объект для сохранения</param>
        /// <returns>Сохранённый базовый объект</returns>
        TBase Save<TKey, TBase, TXref>(TBase baseObject)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;
    }
}