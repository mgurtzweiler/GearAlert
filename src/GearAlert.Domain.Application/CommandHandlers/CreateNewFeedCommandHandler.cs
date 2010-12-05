using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Services.CommandHandlers {
    public class CreateNewFeedCommandHandler : IHandleMessages<CreateNewFeedCommand> {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(CreateNewFeedCommand message) {
            var feed = Feed.Create(message.Name, message.Url, message.LandingUrl);
            Session.Save(feed);
        }
    }
}
