using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using GearAlert.Reporting.Model;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace GearAlert.Configuration {
    public class NHibernateQueryConfiguration {
        private ISessionFactory SessionFactory;
        private string SchemaPath;

        public NHibernateQueryConfiguration Configure(string connectionString, string siteRoot) {
            SchemaPath = siteRoot;
            try
            {
                SessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2008.ConnectionString(x => x.Is(connectionString))
                                  .ProxyFactoryFactory(
                                      "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle"))
                    .Mappings(m => m.AutoMappings.Add(
                        AutoMap.AssemblyOf<Feed>(new CustomMappingConfiguration())
                            .Conventions.AddFromAssemblyOf<PrimaryKeyGeneratorConvention>())
                                       .ExportTo(SchemaPath))
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            } catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }
            return this;
        }

        private void BuildSchema(NHibernate.Cfg.Configuration cfg) {
            var schemaExporter = new SchemaExport(cfg);
            schemaExporter.SetOutputFile(Path.Combine(SchemaPath, "schema.sql"));
            schemaExporter.Create(true, false);
        }

        public ISession OpenSession(string connectionString, string siteRoot) {
            if (SessionFactory == null) Configure(connectionString, siteRoot);
            return SessionFactory.OpenSession();
        }
        public class CustomMappingConfiguration : DefaultAutomappingConfiguration {

            public override bool ShouldMap(Member member) {
                return member.CanWrite && member.IsProperty && base.ShouldMap(member);
            }

            public override bool ShouldMap(System.Type type) {
                return !type.IsAbstract && type.Namespace.EndsWith("Model");
            }
        }

        public class PrimaryKeyGeneratorConvention : IIdConvention {
            public bool Accept(IIdentityInstance id) {
                return true;
            }
            public void Apply(IIdentityInstance id) {
                id.GeneratedBy.GuidComb();
            }
        }
    }

   
}
