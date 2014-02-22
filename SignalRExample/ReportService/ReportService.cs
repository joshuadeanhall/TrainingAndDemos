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
using Microsoft.WindowsAzure;

namespace ReportService
{
    public class ReportService
    {
        public void Start()
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
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
        }

        public void Stop()
        {
            
        }
    }
}
