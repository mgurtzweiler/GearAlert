using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GearAlert.Messages.Events;
using GearAlert.Reporting.Model;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.EventHandlers {
    public class NewFeedCreatedEventHandler : IHandleMessages<INewFeedCreatedEvent>
    {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(INewFeedCreatedEvent message)
        {
            var queryFeed = new Feed() {Name = message.Name, Url = message.Url, LandingPageUrl = message.LandingPageUrl};
            Session.Save(queryFeed);
        }
    }
}
