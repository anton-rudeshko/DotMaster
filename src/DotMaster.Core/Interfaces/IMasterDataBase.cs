namespace DotMaster.Core.Interfaces
{
    public interface IMasterDataBase<TXref, TBase>
        where TXref : ICrossReference<TBase>
        where TBase : IBaseObject
    {
        /// <summary>
        /// Найти base object для переданного xref.
        /// Xref должен иметь SourceKey, по нему будет производится поиск
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        /// <returns>BO если найден, null иначе</returns>
        TBase BaseObjectFor(TXref xref);

        /// <summary>
        /// Создать новый BO на основе переданного xref
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        void CreateBaseObjectFrom(TXref xref);

        /// <summary>
        /// Добавить xref в данный BO, пересчитать доверительные правила
        /// </summary>
        /// <param name="baseObject">Базовый объект</param>
        /// <param name="xref">Перекрёстная ссылка</param>
        void AppendXrefTo(TBase baseObject, TXref xref);
    }
}