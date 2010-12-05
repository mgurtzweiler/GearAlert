using System.IO;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using GearAlert.Domain;
using GearAlert.Domain.Feeds;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace GearAlert.Configuration
{
    public class NHibernateDomainConfiguration {
        private ISessionFactory SessionFactory;
        private string SchemaPath;

        public NHibernateDomainConfiguration Configure(string connectionString, string siteRoot) {
            SchemaPath = siteRoot;
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

            public override bool ShouldMap(System.Type type)
            {
                return !type.IsAbstract && type.GetInterface("IMappable") != null;
            }
        }

        public class PrimaryKeyGeneratorConvention : IIdConvention, IHasOneConvention, IHasManyConvention, IReferenceConvention, IHasManyToManyConvention {
            public bool Accept(IIdentityInstance id) {
                return true;
            }
            public void Apply(IIdentityInstance id) {
                id.GeneratedBy.GuidComb();
            }
            #region IConvention<IOneToOneInspector,IOneToOneInstance> Members

            public void Apply(IOneToOneInstance instance) {
                instance.Cascade.All();
            }

            #endregion

            #region IConvention<IOneToManyCollectionInspector,IOneToManyCollectionInstance> Members

            public void Apply(IOneToManyCollectionInstance instance) {
                instance.Cascade.AllDeleteOrphan();
            }

            #endregion

            #region IConvention<IManyToOneInspector,IManyToOneInstance> Members

            public void Apply(IManyToOneInstance instance) {
                instance.Cascade.All();
            }

            #endregion

            #region IConvention<IManyToManyCollectionInspector,IManyToManyCollectionInstance> Members

            public void Apply(IManyToManyCollectionInstance instance) {
                instance.Cascade.AllDeleteOrphan();
            }

            #endregion
        }
    }

    
}