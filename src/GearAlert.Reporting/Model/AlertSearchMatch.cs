using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Reporting.Model {
        [Serializable]
    public class AlertSearchMatch {
        public virtual Guid Id { get; set; }
        public virtual Guid SubscriberId { get; set; }
        public virtual Guid SubscriptionId { get; set; }
        public virtual Guid AlertId { get; set; }
        public virtual Guid SearchTermId { get; set; }
        public virtual DateTime Timestamp { get; set; }

    }
}
