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
        public void ThrowIfNoParameterlessConstructor()
        {
            Assert.Throws<ArgumentException>(() => new TrustStrategyAttribute(typeof (TestTrustStrategy)));
        }

        [Test]
        public void BeCreatedFromImplementingClass()
        {
            Assert.That(new TrustStrategyAttribute(typeof (TestTrustStrategyParameterless)), Is.Not.Null);
        }

        [Test]
        public void CanCreateStrategyInstance()
        {
            var attribute = new TrustStrategyAttribute(typeof (TestTrustStrategyParameterless));
            var strategyInstance = attribute.GetStrategyInstance();

            Assert.That(strategyInstance, Is.Not.Null);
            Assert.That(strategyInstance, Is.TypeOf<TestTrustStrategyParameterless>());
        }

        private class TestTrustStrategy : TestTrustStrategyParameterless
        {
            public TestTrustStrategy(int a)
            {
            }
        }

        private class TestTrustStrategyParameterless : ITrustStrategy
        {
            public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
                where TBase : class, IBaseObject<TKey, TBase, TXref> 
                where TXref : class, ICrossReference<TKey, TBase, TXref>
            {
                throw new NotImplementedException();
            }
        }
    }
}