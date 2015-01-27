using System;
using Microsoft.WindowsAzure.ServiceRuntime;
using Topshelf;

namespace ReportService
{
    class Program : RoleEntryPoint
    {
        static void Main(string[] args)
        {
            var service = new ReportService();
            service.Start();
            Console.ReadLine();
        }

        public override void Run()  /* Worker Role entry point */
        {
            var service = new ReportService();
                service.Start();
        }
    }
}
