using System.Collections.Generic;

namespace GearAlert.Domain.Feeds
{
    public class Subscriber : EntityBase, IMappable {
        public virtual string Email { get; protected set; }

        protected Subscriber() { }

        public static Subscriber Create(string email) {
            return new Subscriber { Email = email };
        }

    }
}