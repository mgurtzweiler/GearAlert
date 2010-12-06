using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using GearAlert.Infrastructure.Search;

namespace GearAlert.Services {
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SearchService" in code, svc and config file together.
    public class SearchService : ISearchService {
        public void DoWork() {
        }

        public IList<IndexItem> Query(string query)
        {
            var index = new SearchIndex();
            return index.QueryIndex(query).ToList();
        }
    }
}
