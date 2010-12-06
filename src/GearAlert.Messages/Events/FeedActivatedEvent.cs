using System;
using NServiceBus;

namespace GearAlert.Messages.Events
{
    public interface FeedActivatedEvent : IMessage {
        Guid FeedId { get; set; }
    }
}