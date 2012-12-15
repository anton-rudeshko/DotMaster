using System;
using System.Reflection;

namespace DotMaster.Core.Trust.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
    public class GenericTrustStrategyAttribute : AbstractTrustStrategyAttribute
    {
        private static readonly Type TrustStrategyInterface = typeof (ITrustStrategy);

        private static ConstructorInfo _constructor;

        public GenericTrustStrategyAttribute(Type trustStrategyType)
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

        public override ITrustStrategy GetStrategyInstance()
        {
            return (ITrustStrategy) _constructor.Invoke(new object[0]);
        }
    }
}