using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public class TaskCompletedDetails : CompletedDetails
    {        
        public virtual Task Task { get; set; }
        public virtual int TaskId { get; set; }
    }
}
