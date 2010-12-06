using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace GearAlert.Messages.Commands {
    public class CreateNewSubscriptionCommand : IMessage
    {
        public Guid SubscriberId { get; set; }
        public Guid FeedId { get; set; }
        public string Query { get; set; }
    }
}
