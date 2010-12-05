using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace GearAlert.Messages.Events {
    public interface NewFeedCreatedEvent : IMessage {
        Guid FeedId { get; set; }
        string Name { get; set; }
        string Url { get; set; }
        string LandingPageUrl { get; set; }
    }
}
