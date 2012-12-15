using System.Linq;
using DotMaster.Core;
using DotMaster.NHibernate;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace DotMaster.Tests.ManyToOne
{
    [TestFixture]
    public class ManyToOneTest
    {
        private Kernel kernel;
        private ISessionFactory sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var testConfiguration = NHibernateTestHelper.CreateStudentConfiguration();
            NHibernateTestHelper.ExportSchema(testConfiguration);

            sessionFactory = testConfiguration.BuildSessionFactory();
            var masterDB = new NHibernateMasterDB(sessionFactory);

            kernel = new Kernel(masterDB);
        }

        [Test]
        public void SimpleSave()
        {
            // Arrange
            var xref = new StudentXref
                {
                    Source = "Somewhere",
                    SourceKey = "123123",
                    ObjectData = new Student { Name = "Hello MDM" }
                };

            // Act
            kernel.Process<long, Student, StudentXref>(xref);

            // Assert
            var students = sessionFactory.GetCurrentSession().Query<Student>().ToList();

            Assert.That(students, Is.Not.Null);
            Assert.That(students.Count, Is.EqualTo(1));
            Assert.That(students[0].Name, Is.EqualTo("Hello MDM"));
        }
    }
}
