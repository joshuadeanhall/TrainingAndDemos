using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace SimpleServices
{
    public class SignalRService : ServiceBase
    {
        IDisposable SignalR { get; set; }

        public void Start()
        {
            SignalR = WebApp.Start<Startup>("http://localhost:8050/");

            Console.WriteLine("Server running at http://localhost:8050/");
            Console.ReadLine();
        }

        public new void Stop()
        {
            SignalR.Dispose();

            Thread.Sleep(1500);
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {
            Stop();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (SignalR != null)
            {
                SignalR.Dispose();
                SignalR = null;
            }
        }
    }
}
