using System;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
using Moq;
using NUnit.Framework;

namespace DotMaster.Tests
{
    public class TrustStrategyAttributeTest
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void TestNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TrustStrategyAttribute(null));
        }

        [Test]
        public void TestBadType()
        {
            Assert.Throws<ArgumentException>(() => new TrustStrategyAttribute(typeof (Double)));
        }

        [Test]
        public void TestInterfaceType()
        {
            Assert.Throws<ArgumentException>(() => new TrustStrategyAttribute(typeof (ITrustStrategy)));
        }

        [Test]
        public void TestGoodType()
        {
            var strategyAttribute = new TrustStrategyAttribute(typeof (TestTrustStrategy));
            Assert.NotNull(strategyAttribute);
        }

        private class TestTrustStrategy : ITrustStrategy
        {
        }
    }
}