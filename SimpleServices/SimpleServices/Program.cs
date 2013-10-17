using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace SimpleServices
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var arg0 = args[0] ?? string.Empty;
            if (arg0 == "-fake")
            {
                RunFake();
            }
            else
            {
                Run();
            }
            
        }

        public static void Run()
        {
            var ServicesToRun = new ServiceBase[] { new SignalRService() };
            ServiceBase.Run(ServicesToRun);
        }

        public static void RunFake()
        {
            var service = new SignalRService();
            service.Start();

            Console.ReadLine();
        }
    }

    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);

                var hubConfig = new HubConfiguration
                {
                    EnableDetailedErrors = true,
                    EnableJSONP = true
                };

                map.RunSignalR(hubConfig);
            });
        }
    }
}
