using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotMaster.Core.Trust.Attributes;

namespace DotMaster.Core.Trust
{
    public class MemberTrustReader
    {
        public MemberTrust ReadTrust(MemberInfo memberInfo)
        {
            return CreateTrustRule(ReadTrustAttributes(memberInfo));
        }

        public IList<AbstractTrustStrategyAttribute> ReadTrustAttributes(MemberInfo type)
        {
            return type.GetCustomAttributes(true).OfType<AbstractTrustStrategyAttribute>().ToList();
        }

        public MemberTrust CreateTrustRule(ICollection<AbstractTrustStrategyAttribute> attributes)
        {
            if (attributes.Count == 0)
            {
                return null;
            }

            ITrustStrategy baseStrategy = null;
            var strategiesBySource = new Dictionary<string, ITrustStrategy>();

            foreach (var attribute in attributes)
            {
                var source = attribute.ForSource;
                var strategy = attribute.GetStrategyInstance();
                if (strategy == null)
                {
                    throw new InvalidOperationException("No trust strategy instance created by " + attribute.GetType());
                }
                if (string.IsNullOrWhiteSpace(source))
                {
                    if (baseStrategy != null)
                    {
                        throw new InvalidOperationException("Duplicate base strategy (with empty source)");
                    }
                    baseStrategy = strategy;
                }
                else
                {
                    if (strategiesBySource.ContainsKey(source))
                    {
                        throw new InvalidOperationException("Duplicate strategy for source " + source);
                    }
                    strategiesBySource.Add(source, strategy);
                }
            }

            return new MemberTrust(strategiesBySource, baseStrategy);
        }
    }
}