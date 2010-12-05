using System;
using NServiceBus;

namespace GearAlert.Messages.Commands
{
    [Serializable]
    public class RegisterNewAlertCommand : IMessage {
        public Guid FeedId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public string RemoteId { get; set; }
    }
}