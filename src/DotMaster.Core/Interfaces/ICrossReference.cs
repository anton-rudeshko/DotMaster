namespace DotMaster.Core.Interfaces
{
    public interface ICrossReference<TKey>
    {
        TKey BaseObjKey { get; set; }
    }
}