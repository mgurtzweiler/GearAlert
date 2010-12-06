using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearAlert.Domain.Feeds;
using GearAlert.Messages.Commands;
using NHibernate;
using NServiceBus;

namespace GearAlert.Domain.Application.CommandHandlers {
    public class CreateNewSubscriptionCommandHandler :IHandleMessages<CreateNewSubscriptionCommand>{
        public ISession Session { get; set; }

        public CreateNewSubscriptionCommandHandler(ISession session)
        {
            Session = session;
        }

        public void Handle(CreateNewSubscriptionCommand message)
        {
            // get the feed
            var feed = Session.Get<Feed>(message.FeedId);
            // see if we have a subscriber with that email already
            var subscriber = Session.Get<Subscriber>(message.SubscriberId);
            var subscription = Subscription.Create(subscriber, message.Query);
            Session.Save(subscription);
            feed.AddSubscription(subscription);
        }
    }
}
