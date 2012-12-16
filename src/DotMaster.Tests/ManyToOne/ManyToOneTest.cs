using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotMaster.Core;
using DotMaster.NHibernate;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace DotMaster.Tests.ManyToOne
{
    [TestFixture, Category("Long")]
    public class ManyToOneTest
    {
        private Kernel kernel;
        private ISessionFactory sessionFactory;

        [SetUp]
        public void SetUp()
        {
            Debug.WriteLine("Creating student database");

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
            var lastUpdate = DateTime.Now;
            var xref = new StudentXref
                {
                    Source = "MAI", SourceKey = "123123",
                    ObjectData = new Student { Name = "Hello MDM" },
                    LastUpdate = lastUpdate
                };

            // Act
            Process(xref);

            // Assert
            var students = GetAllStudents();

            Assert.That(students, Is.Not.Null);
            Assert.That(students.Count, Is.EqualTo(1));
            Assert.That(students[0].Name, Is.EqualTo("Hello MDM"));
            Assert.That(students[0].LastUpdate, Is.EqualTo(lastUpdate));
        }

        [Test]
        public void SecondSave()
        {
            // Arrange
            var sourceKey = "123123";

            var firstUpdate = DateTime.Now;
            var secondUpdate = firstUpdate.AddDays(3);

            var firstName = "Hello MDM";
            var secondName = "Updated 'Hello MDM'";

            var firstXref = MaiStudent(sourceKey, firstName, firstUpdate);
            var secondXref = MaiStudent(sourceKey, secondName, secondUpdate);

            Process(firstXref);

            // Act
            Process(secondXref);

            // Assert
            var students = GetAllStudents();

            Assert.That(students, Is.Not.Null);
            Assert.That(students.Count, Is.EqualTo(1));
            Assert.That(students[0].Name, Is.EqualTo(secondName));
            Assert.That(students[0].LastUpdate, Is.EqualTo(secondUpdate));
        }

        private void Process(StudentXref xref)
        {
            kernel.Process<int, Student, StudentXref>(xref);
        }

        private List<Student> GetAllStudents()
        {
            return sessionFactory.GetCurrentSession().Query<Student>().ToList();
        }

        private static StudentXref MaiStudent(string sourceKey, string name, DateTime lastUpdate)
        {
            return new StudentXref
                {
                    Source = "MAI",
                    SourceKey = sourceKey,
                    ObjectData = new Student { Name = name },
                    LastUpdate = lastUpdate
                };
        }
    }
}
