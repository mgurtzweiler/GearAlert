using System;
using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using GearAlert.Messages.Events;
using NHibernate;
using NServiceBus;

namespace GearAlert.Domain.Application.CommandHandlers
{
    public class ActivateFeedCommandHandler : IHandleMessages<ActivateFeedCommand> {
        public ISession Session { get; set; }

        public ActivateFeedCommandHandler(ISession session) {
            Session = session;
        }

        public void Handle(ActivateFeedCommand message) {
                var existingFeed = Session.Get<Feed>(message.FeedId);
                existingFeed.Activate();
        }
    }
}