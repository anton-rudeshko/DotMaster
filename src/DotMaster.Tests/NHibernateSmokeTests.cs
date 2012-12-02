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

        [Test]
        public void FindBaseObject()
        {
            // Arrange
            var testXref = new TestXref {SourceKey = "fba", ObjKey = "abf"};
            db.CreateBaseObjectFrom(testXref);

            // Act
            var bo = db.BaseObjectFor<TestBO, TestXref>(testXref);

            // Assert
            Assert.That(bo, Is.Not.Null);
            Assert.That(testXref.SourceKey, Is.EqualTo(bo.ObjKey));
        }
    }
}