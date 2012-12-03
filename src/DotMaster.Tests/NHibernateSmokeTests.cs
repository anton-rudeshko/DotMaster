using System;
using DotMaster.NHibernate;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture]
    public class NHibernateSmokeTests
    {
        private NHibernateMasterDB db;

        [TestFixtureSetUp]
        public static void SetUpFixture()
        {
            var config = NHibernateTestHelper.CreateTestConfiguration<TestBO>();
            NHibernateTestHelper.ExportSchema(config);
            TestSessionFactory.SessionFactory = config.BuildSessionFactory();
        }

        [SetUp]
        public void SetUp()
        {
            db = new NHibernateMasterDB(TestSessionFactory.SessionFactory);
        }

        [Test]
        public void ShouldThrowIfNull()
        {
            Assert.Throws<ArgumentNullException>(() => db.BaseObjectFor<TestBO, TestXref>(null));
        }

        [Test]
        public void ShouldThrowIfNoSrcKey()
        {
            Assert.Throws<ArgumentException>(() => db.BaseObjectFor<TestBO, TestXref>(new TestXref()));
        }
    }
}