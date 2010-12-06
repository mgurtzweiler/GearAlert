using System;
using System.Collections.Generic;

namespace GearAlert.Domain.Feeds
{
    public class Subscriber : EntityBase, IAutoMappable {
        public virtual string Email { get; protected set; }

        protected Subscriber() { }

        public static Subscriber Create(string email) {
            return new Subscriber { Email = email };
        }

    }
}