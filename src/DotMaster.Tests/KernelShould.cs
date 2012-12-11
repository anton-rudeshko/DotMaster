using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
using Moq;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture]
    public class KernelShould
    {

        private Kernel kernel;

        private Mock<TestProvider> provider;
        private Mock<IMasterDataBase> db;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IMasterDataBase>();
            provider = new Mock<TestProvider>();
            kernel = new Kernel(db.Object);
            kernel.RegisterDataProvider(provider.Object);
        }

        [Test]
        public void CheckForBaseObject()
        {
            // Arrange
            var testXref = new TestXref();

            // Act
            provider.Raise(a => a.OnData += null, testXref);

            // Verify
            db.Verify(@base => @base.BaseObjectFor<long, TestBO, TestXref>(testXref));
        }

        [Test]
        public void AddToExisting()
        {
            // Arrange
            var testXref = new TestXref();
            var testBo = new TestBO();
            db.Setup(a => a.BaseObjectFor<long, TestBO, TestXref>(testXref)).Returns(testBo);

            // Act
            provider.Raise(a => a.OnData += null, testXref);

            // Assert
            db.Verify(@base => @base.AppendXrefTo<long, TestBO, TestXref>(testBo, testXref));
        }
    }
}