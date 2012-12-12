using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;

namespace DotMaster.Core.Trust
{
    public class LinearDecreaseAttribute : TrustStrategyAttribute
    {
        public LinearDecreaseAttribute() : base(typeof (LinearDecreaseTrustStrategy))
        {
        }

        public override ITrustStrategy GetStrategyInstance()
        {
            return new LinearDecreaseTrustStrategy();
        }
    }
}