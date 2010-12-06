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
            ScanAndWireUp(kernel);
            kernel.Bind<ISession>().ToMethod(m =>
                                             new NHibernateDomainConfiguration().OpenSession(
                                                 ConfigurationSettings.AppSettings["connectionString"],
                                                 "./NHibernate")).InThreadScope();
            DomainEvents.Container = kernel;
            return kernel;
        }

        public void ScanAndWireUp(IKernel kernel)
        {
            var assembly = GetType().Assembly;
            foreach(var type in assembly.GetTypes())
            {
                var handlesType = type.GetInterface("IHandles`1");
                if (handlesType != null)
                {
                    kernel.Bind(handlesType).To(type);
                }
            }
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