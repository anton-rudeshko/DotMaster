using DotMaster.Example;
using FluentNHibernate.Cfg;
using NHibernate;
using NUnit.Framework;

namespace DotMaster.Tests.StudentExample
{
    [TestFixture]
    public class StudentExampleTest
    {
        private ISessionFactory _sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var configuration = NHibernateTestHelper.CreateTestConfiguration(StudentMappings);
            NHibernateTestHelper.ExportSchema(configuration);

            _sessionFactory = configuration.BuildSessionFactory();
        }

        [Test]
        public void MyTest()
        {
            // Arrange

            // Act

            // Assert
        }

        private static void StudentMappings(MappingConfiguration m)
        {
            m.FluentMappings
             .Add<StudentMap>().Add<StudentXrefMap>()
             .Add<MarkMap>().Add<MarkXrefMap>()
             .Add<StudentTypeMap>();
        }
    }
}
