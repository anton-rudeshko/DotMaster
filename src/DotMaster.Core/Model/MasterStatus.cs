namespace DotMaster.Core.Model
{
    public enum MasterStatus
    {
        /// <summary>
        /// Золотая запись
        /// </summary>
        Consolidated = 1,

        /// <summary>
        /// Отражает готовность базового объекта для процесса поиска совпадений
        /// </summary>
        ReadyForMatch = 4
    }
}
