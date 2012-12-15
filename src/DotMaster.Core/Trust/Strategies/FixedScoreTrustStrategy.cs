using DotMaster.Core.Model;

namespace DotMaster.Core.Trust.Strategies
{
    public class FixedScoreTrustStrategy : ITrustStrategy
    {
        private readonly int _score;

        public FixedScoreTrustStrategy(int score)
        {
            _score = score;
        }

        public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            return _score;
        }
    }
}
