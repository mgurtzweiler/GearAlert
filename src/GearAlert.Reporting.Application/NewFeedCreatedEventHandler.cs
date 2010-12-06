using GearAlert.Messages.Events;
using GearAlert.Reporting.Model;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Reporting.Application {
    public class NewFeedCreatedEventHandler : IHandleMessages<NewFeedCreatedEvent>
    {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(NewFeedCreatedEvent message)
        {
            var queryFeed = new Feed { Id = message.FeedId, Name = message.Name, Url = message.Url, LandingPageUrl = message.LandingPageUrl };
            Session.Save(queryFeed, message.FeedId);
        }
    }
}
