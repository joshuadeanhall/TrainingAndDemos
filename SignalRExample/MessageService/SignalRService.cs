using System;
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
        public void Start()
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
            string url = CloudConfigurationManager.GetSetting("SignalRUrl"); 
            var container = new WindsorContainer();
            var adapter = new WindsorContainerAdapter(container);
            container.Register(
                // Register handlers
                Classes.FromThisAssembly().BasedOn(typeof(IHandleMessages<>)).WithServiceBase().LifestyleTransient()
            );
            Configure.With(adapter)
                      .Transport(t => t.UseAzureServiceBusAndGetInputQueueNameFromAppConfig("Endpoint=sb://messageservice-ns.servicebus.windows.net/;SharedAccessKeyName=All;SharedAccessKey=F7wHHO3jeUhhm8/mOWhT9m3wTFGByuve6e6B9QOEx58="))
                      .MessageOwnership(o => o.FromRebusConfigurationSection())
                      .CreateBus()
                      .Start();

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