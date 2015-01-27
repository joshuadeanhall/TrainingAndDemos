using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Models
{
    public class MaxHoursProjectViewModel : ProjectViewModel
    {
        public decimal MaxHours { get; set; }
        public override string ProjectType
        {
            get { return "Max Hours Project"; }
        }
    }
}