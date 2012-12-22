namespace DotMaster.Core.Match
{
    public interface IMatchRule<in T>
    {
        bool IsMatch(T other);
    }
}