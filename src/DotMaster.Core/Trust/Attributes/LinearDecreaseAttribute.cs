using DotMaster.Core.Trust.Strategies;

namespace DotMaster.Core.Trust.Attributes
{
    /// <summary>
    /// Линейное уменьшение доверительного уровня со временем.
    /// </summary>
    public class LinearDecreaseAttribute : AbstractTrustStrategyAttribute
    {
        /// <summary>
        /// Начальный доверительный уровень (в момент обновления)
        /// </summary>
        public int From { get; set; }

        /// <summary>
        /// Конечный доверительный уровень
        /// </summary>
        public int To { get; set; }

        /// <summary>
        /// Время перехода от <see cref="From"/> к <see cref="To"/>
        /// </summary>
        public int Decay { get; set; }

        /// <summary>
        /// Создать экземпляр стратегии
        /// </summary>
        public override ITrustStrategy GetStrategyInstance()
        {
            return new LinearDecreaseTrustStrategy(From, To, Decay);
        }
    }
}