using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class CreateReportMessage
    {
        public string UserName { get; set; }
        public string ReportName { get; set; }
    }
}
