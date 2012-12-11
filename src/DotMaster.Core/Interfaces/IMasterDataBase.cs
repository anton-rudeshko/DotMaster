using System.Collections.Generic;

namespace DotMaster.Core.Interfaces
{
    public interface IMasterDataBase
    {
        /// <summary>
        /// Найти base object для переданного xref.
        /// Xref должен иметь SourceKey, по нему будет производится поиск
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        /// <returns>BO если найден, null иначе</returns>
        TBase BaseObjectFor<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Создать новый BO на основе переданного xref
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        void CreateBaseObjectFrom<TKey, TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        /// <summary>
        /// Добавить xref в данный BO, пересчитать доверительные правила
        /// </summary>
        /// <param name="baseObject">Базовый объект</param>
        /// <param name="xref">Перекрёстная ссылка</param>
        void AppendXrefTo<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        TXref QueryForXref<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

        IEnumerable<TXref> QueryForXrefs<TKey, TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>;

//        TBase QueryForBaseObject<TKey, TBase, TXref>(string sourceKey, string source)
//            where TBase : class, IBaseObject<TKey, TBase, TXref>
//            where TXref : class, ICrossReference<TKey, TBase, TXref>;
    }
}