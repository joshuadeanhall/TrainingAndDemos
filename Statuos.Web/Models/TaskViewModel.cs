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
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string UserNames { get; set; }
        public decimal EstimatedHours { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ConcreteModelType { get { return this.GetType().ToString(); } }
    }
}