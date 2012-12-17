using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public abstract class CompletedDetails
    {
        public virtual int? Id { get; set; }
        public virtual User CompletedBy { get; set; }
        public virtual int CompletedById { get; set; }
        public virtual DateTime CompletedOn { get; set; }
    }
}
