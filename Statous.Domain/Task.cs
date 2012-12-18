using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public abstract class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual TaskCompletedDetails CompletedDetails { get; set; }
        public decimal EstimatedHours { get; set; }

        public void MarkComplete(TaskCompletedDetails taskCompletedDetails)
        {
            CompletedDetails = taskCompletedDetails;
        }
    }

}
