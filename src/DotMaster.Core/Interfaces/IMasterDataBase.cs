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
            where TXref : class, ICrossReference
            where TBase : class, IBaseObject;

        /// <summary>
        /// Создать новый BO на основе переданного xref
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        void CreateBaseObjectFrom<TXref>(TXref xref)
            where TXref : class, ICrossReference;

        /// <summary>
        /// Добавить xref в данный BO, пересчитать доверительные правила
        /// </summary>
        /// <param name="baseObject">Базовый объект</param>
        /// <param name="xref">Перекрёстная ссылка</param>
        void AppendXrefTo<TBase, TXref>(TBase baseObject, TXref xref)
            where TXref : class, ICrossReference
            where TBase : class, IBaseObject;
    }
}