using GearAlert.Domain.Feeds;
using GearAlert.Messages.Events;
using Ninject;
using NServiceBus;

namespace GearAlert.Domain.Application.EventHandlers
{
    public class NewAlertAddedToFeedHandler : IHandles<NewAlertAddedToFeed> {
        [Inject]
        public IBus Bus { get; set; }

        public void Handle(NewAlertAddedToFeed args) {
            Bus.Publish<NewAlertRegisteredEvent>(e =>
                                                     {
                                                         e.FeedId = args.Feed.Id;
                                                         e.AlertId = args.Alert.Id;
                                                         e.RemoteId = args.Alert.RemoteId;
                                                         e.Title = args.Alert.Title;
                                                         e.Url = args.Alert.Url;
                                                         e.Summary = args.Alert.Summary;
                                                     });
        }
    }
}