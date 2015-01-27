using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Domain
{
    public class BasicProject : Project
    {
        public override string ProjectTypeDescription
        {
            get { return "Basic Project"; }
        }

        public override bool CanChargeHours(decimal hours)
        {
            return true;
        }
    }
}
