using System;

namespace GearAlert.Reporting.Model {
    [Serializable]
    public class Alert {
        public virtual Guid Id { get; set; }
        public virtual Guid FeedId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Summary { get; set; }
        public virtual string Url { get; set; }
        public virtual string RemoteId { get; set; }
        public virtual DateTime Timestamp { get; set; }
    }
}
