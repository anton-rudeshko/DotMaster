using System;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Trust;
using NUnit.Framework;

namespace DotMaster.Tests.Processing
{
    [TestFixture]
    public class TrustReaderTest
    {
        private TrustReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new TrustReader();
        }

        [Test]
        public void NotMasteredTypes([Values(typeof(double), typeof(TrustReader))] Type type)
        {
            Assert.That(reader.TypeIsMastered(type), Is.False);
        }

        [Test]
        public void MasteredTypes([Values(typeof(TestBO))] Type type)
        {
            Assert.That(reader.TypeIsMastered(type), Is.True);
        }

        [Test]
        public void PropertyEquals()
        {
            var prop1 = typeof (A).GetProperty("MyProperty");
            var prop2 = typeof (A).GetProperty("MyProperty");

            Assert.That(prop1, Is.EqualTo(prop2));
        }

        [Test]
        public void PropertyNotEquals()
        {
            var prop1 = typeof (A).GetProperty("MyProperty");
            var prop2 = typeof (B).GetProperty("MyProperty");

            Assert.That(prop1, Is.Not.EqualTo(prop2));
        }

        [Test]
        public void ReadTrustStrategyFromProperty()
        {
            var trustStrategy = reader.ReadTrustRulesFrom(typeof (C).GetProperty("MyProperty"));

            Assert.That(trustStrategy, Is.Not.Null);
            Assert.That(trustStrategy, Is.InstanceOf<ITrustStrategy>());
            Assert.That(trustStrategy, Is.InstanceOf<TestTrustStrategy>());
        }

        [Test]
        public void InheritedTrustStrategyAttribute()
        {
            var trustStrategy = reader.ReadTrustRulesFrom(typeof (C).GetProperty("MyProperty1"));

            Assert.That(trustStrategy, Is.Not.Null);
            Assert.That(trustStrategy, Is.InstanceOf<ITrustStrategy>());
            Assert.That(trustStrategy, Is.InstanceOf<FixedScoreTrustStrategy>());
        }

        [Test]
        public void ReadTrustStrategyFromType()
        {
            var trustStrategy = reader.ReadTrustRulesFrom(typeof (C));

            Assert.That(trustStrategy.Count, Is.EqualTo(3));
        }

        private class A
        {
            public int MyProperty { get; set; }
        }

        private class B
        {
            public int MyProperty { get; set; }
        }

        private class C
        {
            [GenericTrustStrategy(typeof(TestTrustStrategy))]
            public int MyProperty { get; set; }
            
            [FixedScore(10)]
            public int MyProperty1 { get; set; }
            
            [LinearDecrease(From = 90, To = 10, Decay = 50)]
            public int MyProperty2 { get; set; }
            
            public int MyProperty3 { get; set; }
        }
    }

    internal class TestTrustStrategy : ITrustStrategy
    {
        public int GetScore<TKey, TBase, TXref>(TBase baseObject, TXref xref) 
            where TBase : class, IBaseObject<TKey, TBase, TXref> 
            where TXref : class, ICrossReference<TKey, TBase, TXref>
        {
            throw new NotImplementedException();
        }
    }
}