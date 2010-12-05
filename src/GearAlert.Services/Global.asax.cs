using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using GearAlert.Configuration;
using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Wcf;
using Ninject.Modules;

namespace GearAlert.Services {
    public class Global : NinjectWcfApplication
    {

        protected override IKernel CreateKernel()
        {
            var kernel =
                new StandardKernel(new NHibernateModule(ConfigurationManager.AppSettings["connectionString"],
                                                        Server.MapPath("~/App_Data")));
            return kernel;
        }
    }

    internal class NHibernateModule : NinjectModule {
        public string ConnectionString { get; set; }
        public string Root { get; set; }
        internal const string SESSION_KEY = "NHibernate.ISession";
        public NHibernateModule(string connectionString, string root) {
            ConnectionString = connectionString;
            Root = root;
        }

        public override void Load() {
            var cfg = new NHibernateQueryConfiguration();
            Bind<NHibernateQueryConfiguration>().ToConstant(cfg);
            Bind<ISession>().ToMethod(GetRequestSession).InRequestScope();
        }

        private ISession GetRequestSession(IContext ctx) {
             return ctx.Kernel.Get<NHibernateQueryConfiguration>().OpenSession(ConnectionString, Root);
        }
    }
}