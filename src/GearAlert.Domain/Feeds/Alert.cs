using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Domain.Feeds {
    public class Alert : EntityBase, IMappable
    {
        protected virtual string Title { get; set; }
        protected virtual string Summary { get; set; }
        protected virtual string Url { get; set; }
        protected virtual string RemoteId { get; set; }
        protected virtual DateTime Timestamp { get; set; }

        protected Alert() {}
        public Alert(string title, string summary, string url, string remoteId)
        {
            Title = title;
            Url = url;
            Summary = summary;
            RemoteId = remoteId;
            Timestamp = DateTime.Now;
            DomainEvents.Raise(new AlertCreated { Alert = this});
        }
    }

    public class AlertCreated : IDomainEvent
    {
        public Alert Alert {get;set;}
    }
}
