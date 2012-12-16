using System;
using DotMaster.Tests.ManyToOne;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace DotMaster.Tests
{
    public static class NHibernateTestHelper
    {
        public static Configuration CreateTestConfiguration()
        {
            return CreateTestConfiguration(TestMappings);
        }

        public static Configuration CreateStudentConfiguration()
        {
            return CreateTestConfiguration(StudentMappings);
        }

        public static Configuration CreateTestConfiguration(Action<MappingConfiguration> mappings)
        {
            return Fluently.Configure()
                           .Database(OracleClientConfiguration.Oracle10.ConnectionString(TestDataBase).ShowSql())
                           .Mappings(mappings)
                           .CurrentSessionContext<ThreadLocalSessionContext>()
                           .BuildConfiguration();
        }

        private static void StudentMappings(MappingConfiguration m)
        {
            m.FluentMappings.Add<StudentMap>().Add<StudentXrefMap>().Add<LectureMap>().Add<LectureXrefMap>();
        }

        private static void TestMappings(MappingConfiguration m)
        {
            m.FluentMappings.Add<TestBOMap>().Add<TestXrefMap>();
        }

        private static void TestDataBase(OracleConnectionStringBuilder builder)
        {
            builder
                .Server("localhost")
                .Port(1521)
                .Username("dotmaster_test")
                .Password("test_passw0rd");
        }

        public static void ExportSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }

        public static void PrintSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, false);
        }
    }

    public static class TestSessionFactory
    {
        public static ISessionFactory SessionFactory { get; set; }

        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            }

            return SessionFactory.GetCurrentSession();
        }

        public static void DisposeCurrentSession()
        {
            var currentSession = CurrentSessionContext.Unbind(SessionFactory);
            currentSession.Close();
            currentSession.Dispose();
        }
    }
}