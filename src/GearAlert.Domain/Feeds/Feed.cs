using System;
using System.Collections.Generic;
using System.Linq;

namespace GearAlert.Domain.Feeds {
    public class Feed : BaseAggregateRoot
    {
        public virtual FeedInformation Information { get; protected set; }
        protected virtual IList<Alert> Alerts { get; set; }
        protected virtual IList<Subscription> Subscriptions { get; set; }
        protected Feed()
        {
            Alerts = new List<Alert>();
            Subscriptions = new List<Subscription>();
        }

        public static Feed Create(string name, string url, string landingPageUrl)
        {
            var feed = new Feed()
                           {
                               Information = FeedInformation.Create(name, url, landingPageUrl)
                           };
            return feed;
        }

        public virtual void Activate()
        {
            Information.MarkFeedAsActive();
            DomainEvents.Raise(new FeedActivated {Feed = this});
        }

        public virtual void Deactivate() {
            Information.MarkAsDeactivated();
            DomainEvents.Raise(new FeedDeactivated { Feed = this });
        }


        public virtual void ChangeFeedName(string newFeedName)
        {
            if (string.IsNullOrEmpty(newFeedName))
                throw new FeedNameNullOrEmptyException();
            Information.ChangeName(newFeedName);
            DomainEvents.Raise(new FeedNameChanged { Feed = this });
        }

        public virtual void AddAlert(Alert alert)
        {
            Alerts.Add(alert);
            DomainEvents.Raise(new NewAlertAddedToFeed {Feed = this, Alert = alert});
        }

        public virtual void AddSubscription(Subscription subscription)
        {
            Subscriptions.Add(subscription);
            DomainEvents.Raise(new SubscriptionAdded {Feed = this, Subscription = subscription});
        }

        public virtual void RemoveSubscription(Subscription subscription) {
            Subscriptions.Remove(subscription);
            DomainEvents.Raise(new SubscriptionRemoved { Feed = this, Subscription = subscription });
        }

        public virtual void RemoveSubscribersSubscriptions(Subscriber subscriber)
        {
            var subsToRemove = Subscriptions.Where(p => p.Subscriber == subscriber);
            foreach(var sub in subsToRemove)
            {
                Subscriptions.Remove(sub);
            }
            DomainEvents.Raise(new SubscribersSubscriptionsRemoved { Feed = this, Subscriber = subscriber });
        }
    }

    public class SubscribersSubscriptionsRemoved : IDomainEvent
    {
        public Subscriber Subscriber { get; set; }
        public Feed Feed { get; set; }
    }

    public class SubscriptionRemoved : IDomainEvent {
        public Feed Feed { get; set; }
        public Subscription Subscription { get; set; }
    }

    public class SubscriptionAdded : IDomainEvent
    {
        public Feed Feed { get; set; }
        public Subscription Subscription { get; set; }
    }

    public class FeedActivated : IDomainEvent
    {
        public Feed Feed { get; set; }
    }

    public class FeedDeactivated : IDomainEvent {
        public Feed Feed { get; set; }
    }

    public class FeedCreated : IDomainEvent
    {
        public Feed Feed { get; set; }
    }

    public class FeedNameChanged : IDomainEvent
    {
        public Feed Feed {get;set;}
    }

    public class NewAlertAddedToFeed : IDomainEvent
    {
        public Feed Feed { get; set; }
        public Alert Alert { get; set; }
    }
}
