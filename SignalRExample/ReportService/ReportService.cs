using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Rebus;
using Rebus.AzureServiceBus;
using Rebus.Castle.Windsor;
using Rebus.Configuration;

namespace ReportService
{
    public class ReportService
    {
        public void Start()
        {
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
        }

        public void Stop()
        {
            
        }
    }
}
