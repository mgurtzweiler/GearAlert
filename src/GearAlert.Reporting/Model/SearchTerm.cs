using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Reporting.Model {
    [Serializable]
    public class SearchTerm {
        public virtual Guid Id { get; set; }
        public virtual Guid FeedId { get; set; }
        public virtual string Query { get; set; }
    }
}
