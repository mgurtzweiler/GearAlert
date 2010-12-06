using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using NHibernate;
using NServiceBus;

namespace GearAlert.Domain.Application.CommandHandlers
{
    public class RegisterNewAlertCommandHandler : IHandleMessages<RegisterNewAlertCommand> {
        public ISession Session { get; set; }

        public RegisterNewAlertCommandHandler(ISession session) {
            Session = session;
        }

        public void Handle(RegisterNewAlertCommand message)
        {
            var feed = Session.Get<Feed>(message.FeedId);
            var alert = Alert.Create(message.Title, message.Summary, message.Url, message.RemoteId);
            Session.Save(alert);
            feed.AddAlert(alert);
        }
    }
}