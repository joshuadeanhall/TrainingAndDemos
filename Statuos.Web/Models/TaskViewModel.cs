using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuos.Web.Models
{
    public abstract class TaskViewModel
    {
        public int Id { get; set; }        
        public string Title { get; set; }
        public string UserNames { get; set; }
        public decimal EstimatedHours { get; set; }
        public abstract string TaskType { get; }
        public ProjectDetails Project { get; set; }
        public List<UserDetails> Users { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ConcreteModelType { get { return this.GetType().ToString(); } }

        public class UserDetails
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public class ProjectDetails
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Manager { get; set; }
            public string CustomerName { get; set; }
            public decimal EstimatedHours { get; set; }
        }
    }

}