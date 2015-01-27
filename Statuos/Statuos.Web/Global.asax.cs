using Castle.Windsor;
using Castle.Windsor.Installer;
using Statuos.Web.Areas.Admin.Models;
using Statuos.Web.Infrastructure.AutoMapper;
using Statuos.Web.Infrastructure.Helpers.Binders;
using Statuos.Web.Infrastructure.Init;
using Statuos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Statuos.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        internal static IWindsorContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapContainer();
            AutoMapperConfiguration.Configure(_container);
            ModelBinders.Binders.Add(typeof(ProjectViewModel), new ConcreteViewModelBinder());
            ModelBinders.Binders.Add(typeof(TaskViewModel), new ConcreteViewModelBinder());
        }

        private void BootstrapContainer()
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.This());
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}