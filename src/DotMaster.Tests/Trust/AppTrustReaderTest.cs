using System;
using DotMaster.Core.Trust;
using NUnit.Framework;

namespace DotMaster.Tests.Trust
{
    [TestFixture]
    public class AppTrustReaderTest
    {
        private AppTrustReader reader;

        [SetUp]
        public void SetUp()
        {
            reader = new AppTrustReader();
        }
        
        [Test]
        public void NotMasteredTypes([Values(typeof(double), typeof(TypeTrustReader))] Type type)
        {
            Assert.That(reader.TypeIsMastered(type), Is.False);
        }

        [Test]
        public void MasteredTypes([Values(typeof(TestBO))] Type type)
        {
            Assert.That(reader.TypeIsMastered(type), Is.True);
        }
    }
}