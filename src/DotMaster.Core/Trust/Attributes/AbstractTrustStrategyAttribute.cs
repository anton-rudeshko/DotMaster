using System;

namespace DotMaster.Core.Trust.Attributes
{
    /// <summary>
    /// Базовый абстрактный класс для создания аттрибутов, описывающих доверительные стратегии.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public abstract class AbstractTrustStrategyAttribute : Attribute
    {
        /// <summary>
        /// Имя источника, для которого указывается стратегия
        /// Если источник не указан, то применяется как стратегия по умолчанию для всех источников
        /// </summary>
        public string ForSource { get; set; }

        /// <summary>
        /// Создать экземпляр стратегии
        /// Не должно возвращать null
        /// </summary>
        public abstract ITrustStrategy GetStrategyInstance();
    }
}
