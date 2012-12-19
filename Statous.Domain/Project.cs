using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Statuos.Domain
{
    public abstract class Project
    {

        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual decimal EstimatedHours { get; set; }
        public virtual int CustomerId {get;set;}
        public virtual int ProjectManagerId { get; set; }
        public virtual User ProjectManager { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ProjectCompletedDetails CompletedDetails { get; set; }
        public abstract string ProjectTypeDescription { get; }


        public virtual void MarkComplete(ProjectCompletedDetails projectCompletionDetails)
        {
            if (CompletedDetails != null)
            {
                return;
            }
            foreach (var task in Tasks)
            {
                task.MarkComplete(ProjectManager);
            }
            CompletedDetails = projectCompletionDetails;
        }
    }
}
