using System;
using System.Configuration;
using System.Diagnostics;
using GearAlert.Configuration;
using GearAlert.Domain.Application.EventHandlers;
using GearAlert.Domain.Feeds;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Domain.Application
{
    public class ReceivingEndpointConfig :
        IConfigureThisEndpoint,
        AsA_Publisher, 
        IWantCustomInitialization
    {
        public void Init()
        {
            Console.WriteLine(Environment.MachineName);
            log4net.Config.XmlConfigurator.Configure();
            NServiceBus.Configure.With()
                .NinjectBuilder(CreateKernel())
                .XmlSerializer();

            // you can leave the rest of the config off since "AsA_Server" handles it
        }
        protected IKernel CreateKernel() {
            var kernel = new StandardKernel();
            kernel.Bind<IHandles<FeedCreated>>().To<FeedCreatedHandler>();
            kernel.Bind<ISession>().ToMethod(m =>
                                             new NHibernateDomainConfiguration().OpenSession(
                                                 ConfigurationSettings.AppSettings["connectionString"],
                                                 "./NHibernate")).InThreadScope();
            DomainEvents.Container = kernel;
            return kernel;
        }
    }

    public class DomainEventsCleaner : IMessageModule {
        public void HandleBeginMessage() { }

        public void HandleEndMessage() {
            DomainEvents.ClearCallbacks();
        }

        public void HandleError()
        {
            DomainEvents.ClearCallbacks();
        }
    }
}