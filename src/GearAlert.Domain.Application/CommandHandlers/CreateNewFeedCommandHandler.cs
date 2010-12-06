using GearAlert.Domain;
using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using GearAlert.Messages.Events;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Services.CommandHandlers {
    public class CreateNewFeedCommandHandler : IHandleMessages<CreateNewFeedCommand> {
        [Inject]
        public ISession Session { get; set; }
        [Inject]
        public IBus Bus { get; set; }

        public void Handle(CreateNewFeedCommand message)
        {
            var feed = Feed.Create(message.Name, message.Url, message.LandingUrl);
            Session.Save(feed);

            Bus.Publish<NewFeedCreatedEvent>(e =>
            {
                e.FeedId = feed.Id;
                e.LandingPageUrl = feed.Information.LandingPageUrl;
                e.Name = feed.Information.Name;
                e.Url = feed.Information.Url;
            });
        }
    }
}
