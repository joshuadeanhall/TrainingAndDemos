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
        public virtual ICollection<Charge> Charges { get; set; }

        public Task()
        {
            Charges = new List<Charge>();
        }
        public void MarkComplete(User user)
        {
            if (CompletedDetails != null)
            {
                return;
            }
            var taskCompletedDetails = new TaskCompletedDetails() { Task = this, CompletedOn = DateTime.Now, CompletedBy = user };
            CompletedDetails = taskCompletedDetails;
        }

        public bool UserIsAssignedTo(User user)
        {
            return this.Users.Any(u => u.UserName == user.UserName) || this.Project.ProjectManager.UserName == user.UserName;
        }

        public void ChargeHours(decimal hours, User user)
        {
            var charge = new Charge();
            charge.Hours = hours;
            charge.User = user;
            Charges.Add(charge);
        }
    }

}
