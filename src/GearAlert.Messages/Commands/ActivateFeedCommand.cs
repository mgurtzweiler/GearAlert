using System;
using NServiceBus;

namespace GearAlert.Messages.Commands {
    [Serializable]
    public class ActivateFeedCommand : IMessage  {
        public Guid FeedId { get; set; }
    }
}
