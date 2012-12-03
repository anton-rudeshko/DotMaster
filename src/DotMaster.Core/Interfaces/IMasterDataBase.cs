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
        TBase BaseObjectFor<TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>;

        /// <summary>
        /// Создать новый BO на основе переданного xref
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        void CreateBaseObjectFrom<TBase, TXref>(TXref xref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>;

        /// <summary>
        /// Добавить xref в данный BO, пересчитать доверительные правила
        /// </summary>
        /// <param name="baseObject">Базовый объект</param>
        /// <param name="xref">Перекрёстная ссылка</param>
        void AppendXrefTo<TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>;

        TXref QueryForXref<TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>;

        IEnumerable<TXref> QueryForXrefs<TBase, TXref>(string sourceKey, string source)
            where TBase : class, IBaseObject<TBase, TXref>
            where TXref : class, ICrossReference<TBase, TXref>;

//        TBase QueryForBaseObject<TBase, TXref>(string sourceKey, string source)
//            where TBase : class, IBaseObject<TBase, TXref>
//            where TXref : class, ICrossReference<TBase, TXref>;
    }
}