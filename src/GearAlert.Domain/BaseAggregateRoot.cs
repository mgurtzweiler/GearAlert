using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Domain {
    public abstract class BaseAggregateRoot : EntityBase {
    }

    public abstract class EntityBase
    {
        public virtual Guid Id { get; protected set; }
    }
}
