using System;
using System.Linq;
using DotMaster.NHibernate;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace DotMaster.Tests.NHibernate
{
    [TestFixture, Category("Long")]
    public class NHibernateMasterDBTest
    {
        private NHibernateMasterDB masterDB;
        private ISessionFactory sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var configuration = NHibernateTestHelper.CreateTestConfiguration();
            NHibernateTestHelper.ExportSchema(configuration);

            sessionFactory = configuration.BuildSessionFactory();
            masterDB = new NHibernateMasterDB(sessionFactory);
        }

        [Test]
        public void SaveBaseObject()
        {
            // Arrange
            var xref1 = new TestXref { Source = "aaa", SourceKey = "bbb" };
            var xref2 = new TestXref { Source = "aaa", SourceKey = "bbb" };

            var testBO = new TestBO
                {
                    MyProperty = "22",
                    MyProperty2 = "23",
                    LastUpdate = DateTime.Now,
                    Xrefs = new[] { xref1, xref2 }
                };

            xref1.BaseObject = xref2.BaseObject = testBO;

            // Act
            masterDB.Save<int, TestBO, TestXref>(testBO);

            // Assert
            var objKey = testBO.ObjKey;
            Assert.That(objKey, Is.Not.EqualTo(0));

            var savedBO = sessionFactory.GetCurrentSession().Query<TestBO>().Single(x => x.ObjKey == objKey);
            var xrefs = sessionFactory.GetCurrentSession().QueryOver<TestXref>().Where(x => x.BaseObjKey == objKey).List();

            Assert.That(xrefs, Is.Not.Null);
        }
    }
}
