using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Trust
{
    public class FixedScoreAttribute : AbstractTrustStrategyAttribute
    {
        private readonly int _score;

        public FixedScoreAttribute(int score)
        {
            _score = score;
        }

        public override ITrustStrategy GetStrategyInstance()
        {
            return new FixedScoreTrustStrategy(_score);
        }
    }
}