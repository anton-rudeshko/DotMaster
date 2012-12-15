using System;
using System.Collections.Generic;

namespace DotMaster.Core.Trust
{
    /// <summary>
    /// Класс, содержащий метаинформацию о доверительных стратегиях для некоторого объекта (свойства или класса)
    /// </summary>
    public class MemberTrust
    {
        private readonly IDictionary<string, ITrustStrategy> _strategiesBySource;
        private readonly ITrustStrategy _baseStrategy;

        public ITrustStrategy BaseStrategy
        {
            get { return _baseStrategy; }
        }

        public int Count
        {
            get { return _strategiesBySource.Count + (HasBaseStrategy ? 0 : 1); }
        }

        public bool HasBaseStrategy
        {
            get { return BaseStrategy == null; }
        }

        public MemberTrust() : this(new Dictionary<string, ITrustStrategy>())
        {
        }

        public MemberTrust(IDictionary<string, ITrustStrategy> strategiesBySource, ITrustStrategy baseStrategy = null)
        {
            if (strategiesBySource == null)
            {
                throw new ArgumentNullException("strategiesBySource");
            }

            _strategiesBySource = strategiesBySource;
            _baseStrategy = baseStrategy;
        }

        /// <summary>
        /// Получить доверительную стратегию для источника.
        /// Если источник не найден, будет возвращена базовая стратегия для свойства.
        /// Если базовая стратегия не найдена, будет возвращён null.
        /// </summary>
        /// <param name="source">Имя источника</param>
        /// <returns>Найденную стратегию или null</returns>
        public ITrustStrategy GetTrustStrategyFor(string source)
        {
            return _strategiesBySource.ContainsKey(source) ? _strategiesBySource[source] : BaseStrategy;
        }
    }
}