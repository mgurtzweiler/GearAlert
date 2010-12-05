using GearAlert.Domain.Feeds;
using GearAlert.Messages.Events;
using Ninject;
using NServiceBus;

namespace GearAlert.Domain.Application.EventHandlers {
    public class FeedCreatedHandler : IHandles<FeedCreated> {
        [Inject]
        public IBus Bus { get; set; }

        public void Handle(FeedCreated args)
        {
            Bus.Publish<INewFeedCreatedEvent>(e =>
            {
                e.FeedId = args.Feed.Id;
                e.LandingPageUrl = args.Feed.Information.LandingPageUrl;
                e.Name = args.Feed.Information.Name;
                e.Url = args.Feed.Information.Url;
            });
        }
    }
}
