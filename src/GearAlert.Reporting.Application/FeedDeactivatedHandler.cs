using GearAlert.Messages.Events;
using GearAlert.Reporting.Model;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Reporting.Application
{
    public class FeedDeactivatedHandler : IHandleMessages<FeedDeactivatedEvent> {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(FeedDeactivatedEvent message) {
            var feed = Session.Get<Feed>(message.FeedId);
            feed.IsActive = false;
            Session.Save(feed);
        }
    }
}