namespace DotMaster.Core.Interfaces
{
    public interface IBaseObject
    {
        /// <summary>
        /// Новый первичный ключ в системе
        /// </summary>
        string ObjKey { get; set; }

        /// <summary>
        /// Первичный ключ из источника
        /// </summary>
        string SrcKey { get; set; }
    }
}