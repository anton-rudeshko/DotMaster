﻿using System;
using DotMaster.Core.Interfaces;
using DotMaster.Core.Processing;
using Moq;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture]
    public class KernelShould
    {
        public class TestProvider : ISourceDataProvider<TestBO, TestXref>
        {
            public event Action<TestXref> OnData;
        }

        private Kernel kernel;
        private Mock<TestProvider> provider;
        private Mock<IMasterDataBase> db;

        [SetUp]
        public void SetUp()
        {
            kernel = new Kernel(db.Object);
            db = new Mock<IMasterDataBase>();
            provider = new Mock<TestProvider>();
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
            db.Verify(@base => @base.BaseObjectFor<TestBO, TestXref>(testXref));
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