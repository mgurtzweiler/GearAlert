using System;

namespace GearAlert.Reporting.Model {

    [Serializable]
    public class Feed {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string LandingPageUrl { get; set; }
    }
}
