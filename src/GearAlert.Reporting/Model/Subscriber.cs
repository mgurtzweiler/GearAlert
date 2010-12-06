using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GearAlert.Reporting.Model {
        [Serializable]
    public class Subscriber {
        public virtual Guid Id { get; set; }
        public virtual string Email { get; set; }
    }
}
