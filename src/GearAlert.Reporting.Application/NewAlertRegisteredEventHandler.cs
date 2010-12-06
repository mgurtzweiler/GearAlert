using System;
using GearAlert.Infrastructure.Search;
using GearAlert.Messages.Events;
using GearAlert.Reporting.Model;
using NHibernate;
using Ninject;
using NServiceBus;
using System.Linq;

namespace GearAlert.Reporting.Application
{
    public class NewAlertRegisteredEventHandler : IHandleMessages<NewAlertRegisteredEvent> {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(NewAlertRegisteredEvent message) {
            // Use AutoMapper to do all of this?
            var alert = new Alert()
                            {
                                FeedId = message.FeedId,
                                RemoteId = message.RemoteId,
                                Summary = message.Summary,
                                Timestamp = DateTime.Now,
                                Title = message.Title,
                                Url = message.Url
                            };
            Session.Save(alert, message.AlertId);

            // Add the alert to the search index
            var index = new SearchIndex();
            var item = new IndexItem
                           {
                               AlertId = alert.Id.ToString(),
                               FeedId = alert.FeedId.ToString(),
                               Summary = alert.Summary,
                               Title = alert.Title,
                               Timestamp = alert.Timestamp,
                               Url = alert.Url
                           };
            index.AddItemToIndex(item);

            // Get all of the current search terms
            var terms =
                Session.QueryOver<SearchTerm>().Where(p => p.FeedId == alert.FeedId).List();
            // See which search terms match the new alert
            var matches = index.WhichQueriesMatchSingleItem(item,
                                                            terms.ToDictionary(mc => mc.Id.ToString(),
                                                                               mc => mc.Query));
            // Find subscribers for the terms
            var subscriptions =
                Session.QueryOver<Subscription>().Where(p => p.FeedId == alert.FeedId).AndRestrictionOn(
                    m => m.SearchTermId).IsInG(matches).List();

            // Send Emails -- for now save to db
            foreach(var subscription in subscriptions)
            {
                Session.Save(new AlertSearchMatch
                                 {
                                     AlertId = alert.Id,
                                     SubscriberId = subscription.SubscriberId,
                                     SearchTermId = subscription.SearchTermId,
                                     Timestamp = DateTime.Now,
                                     SubscriptionId = subscription.Id
                                 });
            }
        }
    }
}