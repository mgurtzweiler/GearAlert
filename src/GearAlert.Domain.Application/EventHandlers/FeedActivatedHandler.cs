using GearAlert.Domain.Feeds;
using GearAlert.Messages.Events;
using Ninject;
using NServiceBus;

namespace GearAlert.Domain.Application.EventHandlers
{
    public class FeedActivatedHandler : IHandles<FeedActivated> {
        [Inject]
        public IBus Bus { get; set; }

        public void Handle(FeedActivated args) {
            Bus.Publish<FeedActivatedEvent>(e =>
                                                {
                                                    e.FeedId = args.Feed.Id;
                                                });
        }
    }
}