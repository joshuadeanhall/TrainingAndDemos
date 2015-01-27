using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KO_Angular_Demo.Models
{
    public class ProjectViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Effort { get; set; }
        public float Cost { get; set; }
        public string UserName { get; set; }
        public List<TaskViewModel> Tasks { get; set; } 

    }

    public class TaskViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AssignedTo { get; set; }
        public int Effort { get; set; }
        public string ProjectId { get; set; }
    }
}