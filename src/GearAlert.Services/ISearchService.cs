using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GearAlert.Infrastructure.Search;

namespace GearAlert.Services {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISearchService" in both code and config file together.
    [ServiceContract]
    public interface ISearchService {
        [OperationContract]
        IList<IndexItem> Query(string query);
    }
}
