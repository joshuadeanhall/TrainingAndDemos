using System;
using Microsoft.Owin.Hosting;

namespace MessageService
{
    public class SignalRService
    {
        public void Start()
        {

            string url = "http://localhost:8010";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }

        public void Stop()
        {
            
        }
    }
}