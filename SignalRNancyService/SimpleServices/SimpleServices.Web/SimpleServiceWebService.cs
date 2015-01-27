using System;
using System.Threading;
using Nancy.Hosting.Self;

namespace SimpleServices.Web
{
    public class SimpleServiceWebService
    {
        NancyHost Nancy { get; set; }

        public void Start()
        {
            var uri =
                new Uri("http://localhost:3579");

            Nancy = new NancyHost(uri);

            Nancy.Start();
        }

        public void Stop()
        {
            Nancy.Dispose();

            Thread.Sleep(1500);
        }

        protected void Dispose(bool disposing)
        {
            if (Nancy != null)
            {
                Nancy.Dispose();
                Nancy = null;
            }
        }
    }
}
