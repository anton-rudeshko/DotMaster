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
        private Mock<ISourceDataProvider<TestXref>> provider;
        private Mock<IMasterDataBase> db;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IMasterDataBase>();
            provider = new Mock<ISourceDataProvider<TestXref>>();
            kernel = new Kernel(db.Object);
            kernel.RegisterDataProvider<TestBO, TestXref>(provider.Object);
        }

        [Test]
        public void CheckForBaseObject()
        {
            // Arrange
            var testXref = new TestXref();

            // Act
            provider.Raise(a => a.OnData += null, testXref);

            // Verify
            db.Verify(@base => @base.BaseObjectFor<TestBO, TestXref>(testXref));
        }

        [Test]
        public void CreateNew()
        {
            // Arrange
            var testXref = new TestXref();
            db.Setup(a => a.BaseObjectFor<TestBO, TestXref>(testXref)).Returns((TestBO) null);

            // Act
            provider.Raise(a => a.OnData += null, testXref);

            // Assert
            db.Verify(@base => @base.CreateBaseObjectFrom(testXref));
        }

        [Test]
        public void AddToExisting()
        {
            // Arrange
            var testXref = new TestXref();
            var testBo = new TestBO();
            db.Setup(a => a.BaseObjectFor<TestBO, TestXref>(testXref)).Returns(testBo);

            // Act
            provider.Raise(a => a.OnData += null, testXref);

            // Assert
            db.Verify(@base => @base.AppendXrefTo(testBo, testXref));
        }
    }
}