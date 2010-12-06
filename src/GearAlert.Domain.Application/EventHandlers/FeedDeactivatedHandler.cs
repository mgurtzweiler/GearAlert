using GearAlert.Domain.Feeds;
using GearAlert.Messages.Events;
using Ninject;
using NServiceBus;

namespace GearAlert.Domain.Application.EventHandlers
{
    public class FeedDeactivatedHandler : IHandles<FeedDeactivated> {
        [Inject]
        public IBus Bus { get; set; }

        public void Handle(FeedDeactivated args) {
            Bus.Publish<FeedDeactivatedEvent>(e =>
                                                  {
                                                      e.FeedId = args.Feed.Id;
                                                  });
        }
    }
}