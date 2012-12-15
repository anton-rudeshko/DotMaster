using System;
using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Trust
{
    /// <summary>
    /// Базовый абстрактный класс для создания аттрибутов, описывающих доверительные стратегии.
    /// </summary>
    public abstract class AbstractTrustStrategyAttribute : Attribute
    {
        /// <summary>
        /// Имя источника, для которого указывается стратегия
        /// Если источник не указан, то применяется как стратегия по умолчанию для всех источников
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Создать экземпляр стратегии
        /// </summary>
        public abstract ITrustStrategy GetStrategyInstance();
    }
}