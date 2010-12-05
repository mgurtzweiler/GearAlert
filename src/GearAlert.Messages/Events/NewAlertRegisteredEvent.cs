using System;
using NServiceBus;

namespace GearAlert.Messages.Events
{
    public interface NewAlertRegisteredEvent : IMessage {
        Guid FeedId { get; set; }
        Guid AlertId { get; set; }
        string Title { get; set; }
        string Summary { get; set; }
        string Url { get; set; }
        string RemoteId { get; set; }
    }
}