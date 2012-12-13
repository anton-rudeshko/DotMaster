using System;
using System.Reflection;
using DotMaster.Core.Interfaces;

namespace DotMaster.Core.Processing
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class TrustStrategyAttribute : Attribute 
    {
        private static readonly Type TrustStrategyInterface = typeof (ITrustStrategy);

        private static ConstructorInfo _constructor;

        public string Source { get; set; }

        public TrustStrategyAttribute(Type trustStrategyType)
        {
            if (trustStrategyType == null)
            {
                throw new ArgumentNullException("trustStrategyType", "Trust strategy cannot be null");
            }
            if (!TrustStrategyInterface.IsAssignableFrom(trustStrategyType))
            {
                throw new ArgumentException("Trust strategy type must inherit from ITrustStrategy", "trustStrategyType");
            }
            if (trustStrategyType.IsAbstract || trustStrategyType.IsInterface)
            {
                throw new ArgumentException("Trust strategy type must not be abstract or interface type", "trustStrategyType");
            }
            _constructor = trustStrategyType.GetConstructor(Type.EmptyTypes);
            if (_constructor == null)
            {
                throw new ArgumentException("Trust strategy type must have parameterless constructor", "trustStrategyType");
            }
        }

        public virtual ITrustStrategy GetStrategyInstance()
        {
            return (ITrustStrategy) _constructor.Invoke(new object[0]);
        }
    }
}