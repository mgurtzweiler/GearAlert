using System;

namespace GearAlert.Web.Models.Admin
{
    public class CreateNewAlertInput {
        public Guid FeedId { get; set; }
        public string Title { get; set; }
        public string RemoteId { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
    }
}