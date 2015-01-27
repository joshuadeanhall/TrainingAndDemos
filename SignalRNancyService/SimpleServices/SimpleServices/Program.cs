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
using Topshelf;

namespace SimpleServices
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string arg0 = string.Empty;
            if (args.Length > 0)
                arg0 = (args[0] ?? string.Empty).ToLower();

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
            HostFactory.Run(x =>
            {
                x.Service<SignalRService>(s =>
                {
                    s.ConstructUsing(name => new SignalRService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalService();

                x.SetDescription("SignalR Simple Service");
                x.SetDisplayName("SignalRSimple");
                x.SetServiceName("SignalrRSimple");
            });
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
