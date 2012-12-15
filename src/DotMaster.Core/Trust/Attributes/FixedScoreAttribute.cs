using DotMaster.Core.Trust.Strategies;

namespace DotMaster.Core.Trust.Attributes
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