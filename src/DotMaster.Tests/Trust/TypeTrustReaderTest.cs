using DotMaster.Core.Trust;
using DotMaster.Core.Trust.Strategies;
using NUnit.Framework;

namespace DotMaster.Tests.Trust
{
    [TestFixture]
    public class TypeTrustReaderTest
    {
        private TypeTrustReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new TypeTrustReader();
        }

        [Test]
        public void ReadProperty_Single_SourceWins()
        {
            var trustRule = reader.ReadTrustRulesFrom(typeof (TestTypes.C).GetProperty("MyProperty"));

            Assert.That(trustRule.Count, Is.EqualTo(1));
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.Not.Null);
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.InstanceOf<TestTrustStrategy>());
        }

        [Test]
        public void ReadProperty_Single_BaseWins()
        {
            var trustRule = reader.ReadTrustRulesFrom(typeof (TestTypes.C).GetProperty("MyProperty1"));

            Assert.That(trustRule.Count, Is.EqualTo(1));
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.Not.Null);
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.InstanceOf<FixedScoreTrustStrategy>());
        }

        [Test]
        public void ReadProperty_Multiple_SourceWins()
        {
            var trustRule = reader.ReadTrustRulesFrom(typeof (TestTypes.C).GetProperty("MyProperty2"));

            Assert.That(trustRule.Count, Is.EqualTo(2));
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.Not.Null);
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.InstanceOf<TestTrustStrategy>());
        }

        [Test]
        public void ReadProperty_Multiple_BaseWins()
        {
            var trustRule = reader.ReadTrustRulesFrom(typeof (TestTypes.C).GetProperty("MyProperty3"));

            Assert.That(trustRule.Count, Is.EqualTo(3));
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.Not.Null);
            Assert.That(trustRule.GetTrustStrategyFor("AAA"), Is.InstanceOf<LinearDecreaseTrustStrategy>());
        }

        [Test]
        public void ReadType_NoMasteredProps()
        {
            Assert.That(reader.ReadTrustRulesFrom(typeof (TestTypes.A)), Is.Null);
        }

        [Test]
        public void ReadType_Mastered()
        {
            Assert.That(reader.ReadTrustRulesFrom(typeof (TestTypes.C)).Count, Is.EqualTo(3));
        }

        [Test]
        public void ReadAllType_ClassBaseWins()
        {
            var trustStrategy = reader.ReadAllTrustRulesFrom(typeof (TestTypes.C)).GetTrustStrategyFor("NoTrust", "BBB");

            Assert.That(trustStrategy, Is.InstanceOf<FixedScoreTrustStrategy>());
            Assert.That(trustStrategy.GetXrefScore<int, TestBO, TestXref>(null, null), Is.EqualTo(10));
        }

        [Test]
        public void ReadAllType_ClassSourceWins()
        {
            var trustStrategy = reader.ReadAllTrustRulesFrom(typeof (TestTypes.C)).GetTrustStrategyFor("NoTrust", "AAA");

            Assert.That(trustStrategy, Is.InstanceOf<FixedScoreTrustStrategy>());
            Assert.That(trustStrategy.GetXrefScore<int, TestBO, TestXref>(null, null), Is.EqualTo(30));
        }

        [Test]
        public void ReadAllType_PropertySourceWins()
        {
            var trustStrategy = reader.ReadAllTrustRulesFrom(typeof (TestTypes.C)).GetTrustStrategyFor("MyProperty3", "CCC");

            Assert.That(trustStrategy, Is.InstanceOf<FixedScoreTrustStrategy>());
            Assert.That(trustStrategy.GetXrefScore<int, TestBO, TestXref>(null, null), Is.EqualTo(5));
        }
    }
}