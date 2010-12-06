namespace GearAlert.Web.Models.Pubsub {
    public class HubCallbackInputModel {
        public string FeedUrl { get; set; }
        public string FeedTitle { get; set; }
        public string ItemTitle { get; set; }
        public string ItemId { get; set; }
        public string ItemDescription { get; set; }
        public string ItemLink { get; set; }
        public string ItemImageUrl { get; set; }
    }
}
