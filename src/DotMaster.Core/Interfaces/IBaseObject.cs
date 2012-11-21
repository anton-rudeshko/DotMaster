namespace DotMaster.Core.Interfaces
{
    public interface IBaseObject<TKey>
    {
        TKey ObjKey { get; set; }
    }
}