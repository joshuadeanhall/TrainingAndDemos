using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Statuos.Web.Models
{
    public class BasicProjectViewModel : ProjectViewModel
    {

        public override string ProjectType
        {
            get { return "Basic Project"; }
        }
    }
}