namespace DotMaster.Core.Interfaces
{
    public interface ICrossReference<T> where T : IBaseObject
    {
        string BaseObjKey { get; set; }
        ISource Source { get; set; }

        /// <summary>
        /// Первичный ключ из источника
        /// </summary>
        string SourceKey { get; set; }

        T Object { get; set; }
    }
}