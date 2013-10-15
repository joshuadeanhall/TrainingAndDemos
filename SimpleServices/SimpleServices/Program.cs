using System;
using System.Collections.Generic;
using System.Linq;
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
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8050/"))
            {
                Console.WriteLine("Server running at http://localhost:8050/");
                Console.ReadLine();
            }
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
