using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using GearAlert.Reporting.Model;
using NHibernate;
using Ninject;

namespace GearAlert.Services {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "IReportingService" in code, svc and config file together.
    public class ReportingService : IReportingService {
        public ISession Session { get; set; }

        public ReportingService(ISession session)
        {
            Session = session;
        }

        public IList<Feed> GetAllFeeds() {
            return Session.QueryOver<Feed>().List();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite) {
            if (composite == null) {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue) {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
