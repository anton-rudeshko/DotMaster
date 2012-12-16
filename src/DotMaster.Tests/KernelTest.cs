using System;
using System.Collections.Generic;
using DotMaster.Core;
using Moq;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture, Category("Long")]
    public class KernelTest
    {

        private Kernel kernel;

        private TestProvider provider;
        private Mock<IMasterDataBase> db;
        private DateTime now;
        private TestXref testXref;
        private TestBO objectData;
        private TestBO savedBO;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IMasterDataBase>();
            provider = new TestProvider();
            kernel = new Kernel(db.Object);
            kernel.RegisterDataProvider(provider);

            now = DateTime.Now;
            objectData = new TestBO { MyProperty = "new" };
            testXref = new TestXref { SourceKey = "123", ObjectData = objectData, LastUpdate = now };
        }

        [Test]
        public void NewBaseObjectCase()
        {
            // Arrange
            PrepareSavedItems();

            // Act
            provider.Provide(testXref);

            // Verify
            db.Verify(x => x.Save<int, TestBO, TestXref>(It.IsAny<TestBO>()));

            // Assert
            Assert.That(savedBO, Is.Not.Null);
            Assert.That(savedBO.MyProperty, Is.EqualTo("new"));
            Assert.That(savedBO.ObjKey, Is.EqualTo(0));
            Assert.That(savedBO.LastUpdate, Is.EqualTo(now));
            Assert.That(savedBO.Xrefs, Is.Not.Null);
            Assert.That(savedBO.Xrefs.Count, Is.EqualTo(1));

            var savedXref = savedBO.Xrefs[0];
            Assert.That(savedXref, Is.Not.Null);
            Assert.That(savedXref.Source, Is.EqualTo(provider.Source));
            Assert.That(savedXref.ObjectData.MyProperty, Is.EqualTo("new"));
            Assert.That(savedXref.ObjKey, Is.EqualTo(0));
            Assert.That(savedXref.LastUpdate, Is.EqualTo(now));
        }

        [Test]
        public void PresentBaseObjectCase()
        {
            // Arrange
            var presentBO = new TestBO { MyProperty = "old", ObjKey = 123, LastUpdate = now };
            var presentXref = new TestXref
                {
                    BaseObject = presentBO, 
                    BaseObjKey = presentBO.ObjKey, 
                    LastUpdate = now, 
                    ObjKey = 5132, 
                    Source = provider.Source, 
                    SourceKey = testXref.SourceKey, ObjectData = new TestBO { MyProperty = "old" }
                };
            presentBO.Xrefs = new List<TestXref> { presentXref };
            db.Setup(x => x.QueryForXref<int, TestBO, TestXref>(testXref.SourceKey, provider.Source)).Returns(presentXref);
            var newTime = now.AddDays(1);
            testXref.LastUpdate = newTime;

            PrepareSavedItems();

            // Act
            provider.Provide(testXref);

            // Verify
            VerifySaved();

            // Assert
            Assert.That(savedBO, Is.Not.Null);
            Assert.That(savedBO, Is.SameAs(presentBO));
            Assert.That(savedBO.MyProperty, Is.EqualTo("new"));
            Assert.That(savedBO.ObjKey, Is.EqualTo(123));
            Assert.That(savedBO.LastUpdate, Is.EqualTo(newTime));
            Assert.That(savedBO.Xrefs, Is.Not.Null);
            Assert.That(savedBO.Xrefs.Count, Is.EqualTo(1));

            var savedXref = savedBO.Xrefs[0];
            Assert.That(savedXref, Is.Not.Null);
            Assert.That(savedXref, Is.SameAs(presentXref));
            Assert.That(savedXref.Source, Is.EqualTo(provider.Source));
            Assert.That(savedXref.ObjectData.MyProperty, Is.EqualTo("new"));
            Assert.That(savedXref.ObjKey, Is.EqualTo(5132));
            Assert.That(savedXref.LastUpdate, Is.EqualTo(newTime));
        }

        private void VerifySaved()
        {
            db.Verify(x => x.Save<int, TestBO, TestXref>(It.IsAny<TestBO>()));
        }

        private void PrepareSavedItems()
        {
            savedBO = null;
            db.Setup(c => c.Save<int, TestBO, TestXref>(It.IsAny<TestBO>())).Callback<TestBO>(obj => savedBO = obj);
        }
    }
}