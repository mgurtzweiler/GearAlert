using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using NHibernate;
using NServiceBus;

namespace GearAlert.Domain.Application.CommandHandlers
{
    public class DeactivateFeedCommandHandler : IHandleMessages<DeactivateFeedCommand> {
        public ISession Session { get; set; }

        public DeactivateFeedCommandHandler(ISession session) {
            Session = session;
        }

        public void Handle(DeactivateFeedCommand message)
        {
            var existingFeed = Session.Get<Feed>(message.FeedId);
            existingFeed.Deactivate();
        }
    }
}