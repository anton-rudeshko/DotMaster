using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotMaster.Core;
using DotMaster.Core.Trust;
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

            var trust = new AppTrustReader().ReadTrustRulesFrom(typeof(TestBO));
            var trustProcessor = new TrustProcessor(trust);
            kernel = new Kernel(masterDB, trustProcessor);
        }

        [Test]
        public void SingleXref()
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
        public void TwoXrefs()
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

        [Test]
        public void TwoSourceXrefs()
        {
            // Arrange
            var firstXref = MaiStudent("123123", "Hello MDM", DateTime.Now);
            var secondXref = MsuStudent("15623", "Updated 'Hello MDM'", DateTime.Now.AddDays(3));

            Process(firstXref);

            // Act
            Process(secondXref);

            // Assert
            var students = GetAllStudents();
            var xrefs = GetAllXrefs();

            Assert.That(students, Is.Not.Null);
            Assert.That(students.Count, Is.EqualTo(2));

            Assert.That(xrefs, Is.Not.Null);
            Assert.That(xrefs.Count, Is.EqualTo(2));
        }

        private void Process(StudentXref xref)
        {
            kernel.Process<int, Student, StudentXref>(xref);
        }

        private IList<Student> GetAllStudents()
        {
            return sessionFactory.GetCurrentSession().Query<Student>().ToList();
        }

        private IList<StudentXref> GetAllXrefs()
        {
            return sessionFactory.GetCurrentSession().Query<StudentXref>().ToList();
        }

        private static StudentXref MaiStudent(string sourceKey, string name, DateTime lastUpdate)
        {
            return CreateStudent("MAI", sourceKey, name, lastUpdate);
        }

        private static StudentXref MsuStudent(string sourceKey, string name, DateTime lastUpdate)
        {
            return CreateStudent("MSU", sourceKey, name, lastUpdate);
        }

        private static StudentXref CreateStudent(string source, string sourceKey, string name, DateTime lastUpdate)
        {
            return new StudentXref
                {
                    Source = source,
                    SourceKey = sourceKey,
                    ObjectData = new Student { Name = name },
                    LastUpdate = lastUpdate
                };
        }
    }
}
