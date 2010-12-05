namespace GearAlert.Domain.Feeds
{
    public class FeedInformation : EntityBase, IMappable
    {
        public virtual string Name { get; protected set; }
        public virtual string Url { get; protected set; }
        public virtual bool IsActive { get; protected set; }
        public virtual string LandingPageUrl { get; protected set; }
        protected FeedInformation() { }
        public static FeedInformation Create(string name, string url, string landingPageUrl)
        {
            return new FeedInformation()
                       {
                           Name = name,
                           Url = url,
                           LandingPageUrl = landingPageUrl
                       };
        }

        public virtual void MarkFeedAsActive() {
            IsActive = true;
        }

        public virtual void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}