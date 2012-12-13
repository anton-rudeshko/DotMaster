using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;

namespace DotMaster.Core.Trust
{
    public class FixedScoreAttribute : TrustStrategyAttribute
    {
        private readonly int _score;

        public FixedScoreAttribute(int score) : base(typeof (FixedScoreTrustStrategy))
        {
            _score = score;
        }

        public override ITrustStrategy GetStrategyInstance()
        {
            return new FixedScoreTrustStrategy(_score);
        }
    }
}