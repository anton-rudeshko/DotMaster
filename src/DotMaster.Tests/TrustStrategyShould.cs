using System;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
using NUnit.Framework;

namespace DotMaster.Tests
{
    public class TrustStrategyShould
    {
        [Test]
        public void NotBeCreatedFromNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TrustStrategyAttribute(null));
        }

        [Test]
        public void NotBeCreatedFromTypeNotInheritingITrustStrategy()
        {
            Assert.Throws<ArgumentException>(() => new TrustStrategyAttribute(typeof (Double)));
        }

        [Test]
        public void NotBeCreatedFromInterface()
        {
            Assert.Throws<ArgumentException>(() => new TrustStrategyAttribute(typeof (ITrustStrategy)));
        }

        [Test]
        public void BeCreatedFromImplementingClass()
        {
            var strategyAttribute = new TrustStrategyAttribute(typeof (TestTrustStrategy));
            Assert.NotNull(strategyAttribute);
        }

        private class TestTrustStrategy : ITrustStrategy
        {
        }
    }
}