using System;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
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
        public void ReadTrustStrategy()
        {
            var trustStrategy = reader.ReadTrustRulesFrom(typeof (C).GetProperty("MyProperty"));

            Assert.That(trustStrategy, Is.Not.Null);
            Assert.That(trustStrategy, Is.InstanceOf<ITrustStrategy>());
            Assert.That(trustStrategy, Is.InstanceOf<TestTrustStrategy>());
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
            [TrustStrategy(typeof(TestTrustStrategy))]
            public int MyProperty { get; set; }
        }
    }

    internal class TestTrustStrategy : ITrustStrategy
    {
    }
}