using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Domain
{
    public class MaxHoursProject : Project
    {
        public decimal MaxHours { get; set; }
        public override string ProjectTypeDescription
        {
            get { return "Max Hours project"; }
        }

        public override bool AddTask(Task task)
        {
            //If existing tasks plus new tasks is more than the max hours allowed by this project return false and don't save the task
            if (Tasks.Sum(t => t.EstimatedHours) + task.EstimatedHours > MaxHours)
                return false;
            else
            {
                return base.AddTask(task);
            }
        }

        public override bool CanChargeHours(decimal hours)
        {
            if (Tasks.SelectMany(t => t.Charges).Sum(c => c.Hours) + hours > MaxHours)
                return false;
            return true;
        }
    }
}
