using DotMaster.Core.Model;

namespace DotMaster.Core.Trust.Strategies
{
    public class LinearDecreaseTrustStrategy : ITrustStrategy
    {
        private readonly int _startScore;
        private int _endScore;
        private int _decay;

        public LinearDecreaseTrustStrategy(int startScore, int endScore, int decay)
        {
            _startScore = startScore;
            _endScore = endScore;
            _decay = decay;
        }

        public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            // todo
            return _startScore;
        }
    }
}