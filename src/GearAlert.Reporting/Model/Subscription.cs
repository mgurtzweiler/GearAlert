using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Reporting.Model {
        [Serializable]
    public class Subscription {
        public virtual Guid Id { get; set; }
        public virtual Guid FeedId { get; set; }
        public virtual Guid SearchTermId { get; set; }
        public virtual Guid SubscriberId { get; set; }
        public virtual DateTime Created { get; set; }
    }
}
