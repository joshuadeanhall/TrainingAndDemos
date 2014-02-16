using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Rebus.AzureServiceBus.Queues;
using Rebus.Castle.Windsor;
using Rebus.Configuration;
using SignalRExample.Infrastructure;

namespace SignalRExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new WindsorContainer();
            

            RegisterBus(container);

            container.Register(Classes.FromThisAssembly()
                                .BasedOn<IController>()
                                .LifestyleTransient()
                                );

            var customControllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(customControllerFactory);
        }

        private void RegisterBus(IWindsorContainer container)
        {
            var adapter = new WindsorContainerAdapter(container);
            Configure.With(adapter)
                      .Transport(t => t.UseAzureServiceBusInOneWayClientMode("Endpoint=sb://messageservice-ns.servicebus.windows.net/;SharedAccessKeyName=All;SharedAccessKey=/="))
                      .MessageOwnership(o => o.FromRebusConfigurationSection())
                      .CreateBus()
                      .Start();
        }
    }
}
