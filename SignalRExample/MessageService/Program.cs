using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.WindowsAzure.ServiceRuntime;
using Topshelf;

namespace MessageService
{
    class Program : RoleEntryPoint
    {
        static void Main(string[] args)
        {
           
                SignalRService service = new SignalRService();
                service.Start();
            }

        public override void Run()
        {
            var service = new SignalRService();
            service.Start();
        }
    }
}
