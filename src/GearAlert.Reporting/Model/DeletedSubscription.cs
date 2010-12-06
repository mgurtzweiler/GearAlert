using System;

namespace GearAlert.Reporting.Model {
    [Serializable]
    public class DeletedSubscription {
        public virtual Guid Id { get; set; }
        public virtual Guid SubscriberId { get; set; }
        public virtual DateTime Timestamp { get; set; }
    }
}