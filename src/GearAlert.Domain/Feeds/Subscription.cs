using System;

namespace GearAlert.Domain.Feeds
{
    public class Subscription : EntityBase, IAutoMappable {
        public virtual string Query { get; protected set; }
        public virtual Subscriber Subscriber { get; protected set; }
        public virtual DateTime Created { get; protected set; }

        protected Subscription() { }

        public static Subscription Create(Subscriber subscriber, string query)
        {
            return new Subscription {Subscriber = subscriber, Query = query, Created = DateTime.Now };
        }
    }
}