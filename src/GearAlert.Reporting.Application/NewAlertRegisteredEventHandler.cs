using GearAlert.Messages.Events;
using NHibernate;
using Ninject;
using NServiceBus;

namespace GearAlert.Reporting.Application
{
    public class NewAlertRegisteredEventHandler : IHandleMessages<NewAlertRegisteredEvent> {
        [Inject]
        public ISession Session { get; set; }

        public void Handle(NewAlertRegisteredEvent message) {
            // Add the alert to the search index

            // Get all of the current search terms

            // See which search terms match the new alert

            // Find subscribers for the terms

            // Send Emails
        }
    }
}