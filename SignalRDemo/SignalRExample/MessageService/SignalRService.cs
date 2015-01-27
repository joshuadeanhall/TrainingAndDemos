using System;
using System.Diagnostics;
using System.Threading;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Owin.Hosting;
using Microsoft.WindowsAzure;
using Rebus;
using Rebus.AzureServiceBus.Queues;
using Rebus.Castle.Windsor;
using Rebus.Configuration;

namespace MessageService
{
    public class SignalRService
    {
        ManualResetEvent CompletedEvent = new ManualResetEvent(false);
        public void Start()
        {
            System.Diagnostics.Trace.TraceError("ZOMG THIS IS BAD");
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            string url = CloudConfigurationManager.GetSetting("SignalRUrl"); 
            var container = new WindsorContainer();
            var adapter = new WindsorContainerAdapter(container);
            container.Register(
                // Register handlers
                Classes.FromThisAssembly().BasedOn(typeof(IHandleMessages<>)).WithServiceBase().LifestyleTransient()
            );
            Configure.With(adapter)
                      .Transport(t => t.UseAzureServiceBusAndGetInputQueueNameFromAppConfig(connectionString))
                      .MessageOwnership(o => o.FromRebusConfigurationSection())
                      .CreateBus()
                      .Start();
            try
            {
                using (WebApp.Start(url))
                {
                    Console.WriteLine("Server running on {0}", url);
                    CompletedEvent.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("An error occured with webappstart " + ex.Message);
            }
        }

        public void Stop()
        {
            
        }
    }
}