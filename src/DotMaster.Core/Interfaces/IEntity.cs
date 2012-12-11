namespace DotMaster.Core.Interfaces
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Первичный ключ любого объекта в DotMaster
        /// </summary>
        TKey ObjKey { get; set; }
    }
}