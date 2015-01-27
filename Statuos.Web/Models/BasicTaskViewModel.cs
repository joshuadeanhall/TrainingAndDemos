using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Models
{
    public class BasicTaskViewModel : TaskViewModel
    {
        public override string TaskType
        {
            get { return "Basic"; }
        }
    }
}