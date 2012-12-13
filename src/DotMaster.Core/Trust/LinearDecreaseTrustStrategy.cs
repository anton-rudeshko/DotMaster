using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Trust
{
    public class LinearDecreaseTrustStrategy : ITrustStrategy
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

        public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
            where TBase : class, IBaseObject<TKey, TBase, TXref>
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            // todo
            return StartScore;
        }
    }
}