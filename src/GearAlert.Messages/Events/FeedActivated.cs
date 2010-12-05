using System;
using NServiceBus;

namespace GearAlert.Messages.Events
{
    public interface FeedActivated : IMessage {
        Guid FeedId { get; set; }
    }
}