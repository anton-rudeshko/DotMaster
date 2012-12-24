using DotMaster.Social;
using FluentNHibernate.Cfg;
using NHibernate;
using NUnit.Framework;

namespace DotMaster.Tests.SocialExample
{
    [TestFixture]
    public class SocialExampleTest
    {
        private ISessionFactory _sessionFactory;

        [SetUp]
        public void SetUp()
        {
            var configuration = NHibernateTestHelper.CreateTestConfiguration(SocialMappings);
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

        private static void SocialMappings(MappingConfiguration m)
        {
            m.FluentMappings
             .Add<ProfileMap>().Add<ProfileXrefMap>()
             .Add<AddressMap>().Add<AddressXrefMap>()
             .Add<CountryMap>();
        }
    }
}
