using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
using Moq;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture]
    public class KernelShould
    {
        private Kernel<TestXref, TestBO> kernel;
        private Mock<ISourceDataProvider<TestXref, TestBO>> provider;
        private Mock<IMasterDataBase<TestXref, TestBO>> db;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IMasterDataBase<TestXref, TestBO>>();
            provider = new Mock<ISourceDataProvider<TestXref, TestBO>>();
            kernel = new Kernel<TestXref, TestBO>(db.Object);
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
            db.Verify(@base => @base.BaseObjectFor(testXref));
        }

        [Test]
        public void CreateNew()
        {
            // Arrange
            var testXref = new TestXref();
            db.Setup(a => a.BaseObjectFor(testXref)).Returns((TestBO) null);

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
            db.Setup(a => a.BaseObjectFor(testXref)).Returns(testBo);

            // Act
            provider.Raise(a => a.OnData += null, testXref);

            // Assert
            db.Verify(@base => @base.AppendXrefTo(testBo, testXref));
        }
    }

    public class TestBO : IBaseObject
    {
        public string ObjKey { get; set; }
        public string SrcKey { get; set; }
    }

    public class TestXref : ICrossReference<TestBO>
    {
        public string BaseObjKey { get; set; }
        public ISource Source { get; set; }
        public string SourceKey { get; set; }
        public TestBO Object { get; set; }
    }
}