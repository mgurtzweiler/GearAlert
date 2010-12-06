using System;
using NServiceBus;

namespace GearAlert.Messages.Events
{
    public interface FeedDeactivatedEvent : IMessage {
        Guid FeedId { get; set; }
    }
}