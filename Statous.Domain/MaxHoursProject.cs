using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuos.Domain
{
    public class MaxHoursProject : Project
    {
        public override string ProjectTypeDescription
        {
            get { return "Max Hours project"; }
        }

        public decimal MaxHours { get; set; }
    }
}
