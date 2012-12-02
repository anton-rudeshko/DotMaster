namespace DotMaster.Core.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Первичный ключ любого объекта в DotMaster
        /// </summary>
        string ObjKey { get; set; }
    }
}