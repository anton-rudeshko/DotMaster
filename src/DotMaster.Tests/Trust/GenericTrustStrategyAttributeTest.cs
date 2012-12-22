using System;
using DotMaster.Core.Model;
using DotMaster.Core.Trust;
using DotMaster.Core.Trust.Attributes;
using NUnit.Framework;

namespace DotMaster.Tests.Trust
{
    public class GenericTrustStrategyAttributeTest
    {
        [Test]
        public void NotBeCreatedFromNull()
        {
            Assert.Throws<ArgumentNullException>(() => new GenericTrustStrategyAttribute(null));
        }

        [Test]
        public void NotBeCreatedFromTypeNotInheritingITrustStrategy()
        {
            Assert.Throws<ArgumentException>(() => new GenericTrustStrategyAttribute(typeof (Double)));
        }

        [Test]
        public void NotBeCreatedFromInterface()
        {
            Assert.Throws<ArgumentException>(() => new GenericTrustStrategyAttribute(typeof (ITrustStrategy)));
        }

        [Test]
        public void ThrowIfNoParameterlessConstructor()
        {
            Assert.Throws<ArgumentException>(() => new GenericTrustStrategyAttribute(typeof (TestTrustStrategy)));
        }

        [Test]
        public void BeCreatedFromImplementingClass()
        {
            Assert.That(new GenericTrustStrategyAttribute(typeof (TestTrustStrategyParameterless)), Is.Not.Null);
        }

        [Test]
        public void CanCreateStrategyInstance()
        {
            var attribute = new GenericTrustStrategyAttribute(typeof (TestTrustStrategyParameterless));
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
            public int GetXrefScore<TKey, TBase, TXref>(TBase baseObject, TXref xref)
                where TBase : class, IBaseObject<TKey, TBase, TXref> 
                where TXref : class, ICrossReference<TKey, TBase, TXref>
            {
                throw new NotImplementedException();
            }

            public object GetWinValue<TKey, TBase, TXref>(TBase baseObject, TXref xref)
                where TBase : class, IBaseObject<TKey, TBase, TXref>
                where TXref : class, ICrossReference<TKey, TBase, TXref>
            {
                throw new NotImplementedException();
            }
        }
    }
}