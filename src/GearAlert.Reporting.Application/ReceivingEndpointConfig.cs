using System.Configuration;
using GearAlert.Configuration;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Services
{
    public class ReceivingEndpointConfig :
        IConfigureThisEndpoint,
        AsA_Server,
        IWantCustomInitialization
    {
        public void Init() {
            log4net.Config.XmlConfigurator.Configure();

            NServiceBus.Configure.With()
                .NinjectBuilder(CreateKernel())
                .XmlSerializer();

            // you can leave the rest of the config off since "AsA_Server" handles it
        }
        protected IKernel CreateKernel() {
            var kernel = new StandardKernel();
            kernel.Bind<ISession>().ToMethod(m =>
                                             new NHibernateQueryConfiguration().OpenSession(
                                                 ConfigurationSettings.AppSettings["connectionString"],
                                                 "./NHibernate")).InThreadScope();
            return kernel;
        }
    }
}