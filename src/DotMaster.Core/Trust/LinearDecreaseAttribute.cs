using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;

namespace DotMaster.Core.Trust
{
    public class LinearDecreaseAttribute : TrustStrategyAttribute
    {
        /// <summary>
        /// Начальный доверительный уровень (в момент обновления)
        /// </summary>
        public int StartScore { get; set; }

        /// <summary>
        /// Конечный доверительный уровень
        /// </summary>
        public int EndScore { get; set; }

        /// <summary>
        /// Время перехода от <see cref="StartScore"/> к <see cref="EndScore"/>
        /// </summary>
        public int Decay { get; set; }

        public LinearDecreaseAttribute() : base(typeof (LinearDecreaseTrustStrategy))
        {
        }

        public override ITrustStrategy GetStrategyInstance()
        {
            return new LinearDecreaseTrustStrategy(StartScore, EndScore, Decay);
        }
    }
}