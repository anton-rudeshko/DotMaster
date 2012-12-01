using DotMaster.Example;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace DotMaster.Tests
{
    [TestFixture]
    public class GenerateSchema
    {
        [Test]
        public void GenerateMusicSchema()
        {
            var configuration = Fluently.Configure()
                .Database(OracleClientConfiguration.Oracle10.ConnectionString(LocalHost))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Track>())
                .BuildConfiguration();

            PrintSchema(configuration);
        }

        private void LocalHost(OracleConnectionStringBuilder builder)
        {
            builder
                .Server("localhost")
                .Port(1521)
                .Username("dotmaster")
                .Password("passw0rd");
        }

        private static void ExportSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }

        private static void PrintSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, false);
        }
    }
}