using System;

namespace GearAlert.Domain.Feeds
{
    public class Subscription : EntityBase, IMappable {
        public virtual SearchTerm SearchTerm { get; protected set; }
        public virtual Subscriber Subscriber { get; protected set; }
        public virtual DateTime Created { get; protected set; }
        protected Subscription() { }

        public static Subscription Create(SearchTerm term, Subscriber subscriber)
        {
            return new Subscription {SearchTerm = term, Subscriber = subscriber, Created = DateTime.Now };
        }
    }
}