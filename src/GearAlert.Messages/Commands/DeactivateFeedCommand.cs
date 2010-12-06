using System;
using NServiceBus;

namespace GearAlert.Messages.Commands
{
    [Serializable]
    public class DeactivateFeedCommand : IMessage  {
        public Guid FeedId { get; set; }
    }
}