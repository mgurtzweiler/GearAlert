using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Domain.Feeds {
    public class Alert : EntityBase, IMappable
    {
        public virtual string Title { get; protected set; }
        public virtual string Summary { get; protected set; }
        public virtual string Url { get; protected set; }
        public virtual string RemoteId { get; protected set; }
        public virtual DateTime Timestamp { get; protected set; }

        protected Alert() {}
        public static Alert Create(string title, string summary, string url, string remoteId)
        {
            return new Alert
                       {
                           Title = title,
                           Url = url,
                           Summary = summary,
                           RemoteId = remoteId,
                           Timestamp = DateTime.Now
                       };
        }
    }
}
