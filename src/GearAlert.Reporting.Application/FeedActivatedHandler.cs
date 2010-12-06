using GearAlert.Messages.Events;
using GearAlert.Reporting.Model;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Reporting.Application
{
    public class FeedActivatedHandler : IHandleMessages<FeedActivatedEvent> {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(FeedActivatedEvent message)
        {
            var feed = Session.Get<Feed>(message.FeedId);
            feed.IsActive = true;
            Session.Save(feed);
        }
    }
}