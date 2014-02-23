using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.WindowsAzure;
using Rebus;
using Rebus.AzureServiceBus;
using Rebus.Castle.Windsor;
using Rebus.Configuration;

namespace MessageServiceWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new WindsorContainer();
            RegisterBus(container);



        }

        private void RegisterBus(IWindsorContainer container)
        {
            var connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
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
    }
}
