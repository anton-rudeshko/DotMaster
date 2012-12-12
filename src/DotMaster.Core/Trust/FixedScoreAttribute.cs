using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;

namespace DotMaster.Core.Trust
{
    public class FixedScoreAttribute : TrustStrategyAttribute
    {
        public FixedScoreAttribute() : base(typeof (FixedScoreTrustStrategy))
        {
        }

        public override ITrustStrategy GetStrategyInstance()
        {
            return new FixedScoreTrustStrategy();
        }
    }
}