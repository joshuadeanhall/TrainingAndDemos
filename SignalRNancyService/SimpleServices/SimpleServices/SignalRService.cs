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
    public class SignalRService
    {
        IDisposable SignalR { get; set; }

        public void Start()
        {
            SignalR = WebApp.Start<Startup>("http://localhost:8050/");

            Console.WriteLine("Server running at http://localhost:8050/");
            Console.ReadLine();
        }

        public void Stop()
        {
            SignalR.Dispose();

            Thread.Sleep(1500);
        }

        protected void Dispose(bool disposing)
        {
            if (SignalR != null)
            {
                SignalR.Dispose();
                SignalR = null;
            }
        }
    }
}
