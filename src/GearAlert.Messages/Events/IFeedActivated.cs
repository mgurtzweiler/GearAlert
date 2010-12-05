using System;
using NServiceBus;

namespace GearAlert.Messages.Events
{
    public interface IFeedActivated : IMessage {
        Guid FeedId { get; set; }
    }
}