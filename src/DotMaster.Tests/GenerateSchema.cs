using DotMaster.Example;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture]
    public class GenerateSchema
    {
        [Test]
        public void GenerateMusicSchema()
        {
            NHibernateTestHelper.PrintSchema(NHibernateTestHelper.CreateTestConfiguration<Track>());
        }
    }
}