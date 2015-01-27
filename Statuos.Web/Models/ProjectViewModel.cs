﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Statuos.Web.Models
{
    public abstract class ProjectViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public SelectList ProjectTypes { get; set; }
        public string UserName { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }
        [DisplayName("Estimated Hours")]
        public decimal EstimatedHours { get; set; }
        public abstract string ProjectType { get; }
        public IEnumerable<TaskViewModel> Tasks { get; set; }

        public List<ProjectChargeDetails> Charges { get; set; }

        [HiddenInput(DisplayValue = false)]
        [MinLength(1)]
        public string ConcreteModelType { get { return this.GetType().ToString(); } }

        public class ProjectChargeDetails
        {
            [DisplayName("Title")]
            public string TaskTitle { get; set; }
            public decimal Hours { get; set; }
            public string User { get; set; }
            public DateTime Date { get; set; }
        }
    }
}