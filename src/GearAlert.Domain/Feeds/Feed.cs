using System;
using System.Collections.Generic;

namespace GearAlert.Domain.Feeds {
    public class Feed : BaseAggregateRoot, IMappable
    {
        public virtual string Name { get; protected set; }
        public virtual string Url { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        public virtual string LandingPageUrl { get; protected set; }
        protected virtual IList<Alert> Alerts { get; set; }

        protected Feed() {}
        public Feed(string name, string url, string landingPageUrl)
        {
            Name = name;
            Url = url;
            LandingPageUrl = landingPageUrl;
            DomainEvents.Raise(new FeedCreated() { Feed = this});
        }

        public virtual void MarkFeedAsActive()
        {
            IsActive = true;
        }

        public virtual void ChangeFeedName(string newFeedName)
        {
            if (string.IsNullOrEmpty(newFeedName))
                throw new FeedNameNullOrEmptyException();
            Name = newFeedName;
            DomainEvents.Raise(new FeedNameChanged { Feed = this });
        }

        public virtual void AddNewAlert(Alert alert)
        {
            Alerts.Add(alert);
            DomainEvents.Raise<NewAlertAddedToFeed>(new NewAlertAddedToFeed {Feed = this, Alert = alert});
        }
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
