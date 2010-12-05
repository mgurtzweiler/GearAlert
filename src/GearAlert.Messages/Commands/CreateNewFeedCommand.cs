using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace GearAlert.Messages.Commands {
    [Serializable]
    public class CreateNewFeedCommand : IMessage {
        public string Name { get; set; }
        public string Url { get; set; }
        public string LandingUrl { get; set; }
    }
}
