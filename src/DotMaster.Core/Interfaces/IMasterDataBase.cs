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
        IBaseObject BaseObjectFor(ICrossReference xref);

        /// <summary>
        /// Создать новый BO на основе переданного xref
        /// </summary>
        /// <param name="xref">Перекрёстная ссылка</param>
        void CreateBaseObjectFrom(ICrossReference xref);

        /// <summary>
        /// Добавить xref в данный BO, пересчитать доверительные правила
        /// </summary>
        /// <param name="baseObject">Базовый объект</param>
        /// <param name="xref">Перекрёстная ссылка</param>
        void AppendXrefTo(IBaseObject baseObject, ICrossReference xref);
    }
}