using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GearAlert.Messages.Commands;
using GearAlert.Web.Models.Home;
using Ninject;
using Ninject.Web.Mvc;
using NServiceBus;

namespace GearAlert.Web {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : NinjectHttpApplication
    {
        private IBus Bus;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected override void OnApplicationStarted() {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            Mapper.CreateMap<AddNewFeedInput, CreateNewFeedCommand>();
            Mapper.AssertConfigurationIsValid();
            base.OnApplicationStarted();
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            var bus = Configure.WithWeb()
                .DefaultBuilder()
                .Log4Net()
                .XmlSerializer()
                .MsmqTransport()
                .IsTransactional(true)
                .PurgeOnStartup(false)
                .UnicastBus()
                .ImpersonateSender(false)
                .LoadMessageHandlers()
                .CreateBus();
            Bus = bus.Start();
            kernel.Bind<IBus>().ToConstant(Bus);
            return kernel;
        }
    }
}