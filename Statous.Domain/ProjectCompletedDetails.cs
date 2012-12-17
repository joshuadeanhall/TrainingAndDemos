using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public class ProjectCompletedDetails : CompletedDetails
    {
        public virtual Project Project { get; set; }
        public virtual int ProjectId { get; set; }
    }
}
