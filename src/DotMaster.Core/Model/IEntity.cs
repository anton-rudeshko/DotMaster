namespace DotMaster.Core.Model
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// Первичный ключ любого объекта в DotMaster
        /// </summary>
        TKey ObjKey { get; set; }
    }
}