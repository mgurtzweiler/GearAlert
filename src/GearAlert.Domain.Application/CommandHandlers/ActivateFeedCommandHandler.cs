using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using GearAlert.Messages.Events;
using NHibernate;
using NServiceBus;

namespace GearAlert.Domain.Application.CommandHandlers
{
    public class ActivateFeedCommandHandler : IHandleMessages<ActivateFeedCommand> {
        public ISession Session { get; set; }
        public IBus Bus { get; set; }

        public ActivateFeedCommandHandler(ISession session, IBus bus) {
            Session = session;
            Bus = bus;
        }

        public void Handle(ActivateFeedCommand message) {
            using (var tx = Session.BeginTransaction())
            {
                var existingFeed = Session.Get<Feed>(message.FeedId);
                existingFeed.MarkFeedAsActive();
                Bus.Publish<IFeedActivated>(e =>
                                                {
                                                    e.FeedId = existingFeed.Id;
                                                });
                tx.Commit();
            }
        }
    }
}