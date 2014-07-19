using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using KO_Angular_Demo.Models;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Driver;
using Ninject;
using Ninject.Web.Common;

namespace KO_Angular_Demo
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            var connectionstring = ConfigurationManager.AppSettings.Get("MongoDatabaseUrl") ??
                       ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                       ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            //TODO pull blog name from app.config
            kernel.Bind<MongoDatabase>().ToConstant(server.GetDatabase(url.DatabaseName)).InRequestScope();

            kernel.Bind<UserStore<ApplicationUser>>()
                .ToMethod(c => new UserStore<ApplicationUser>(kernel.Get<MongoDatabase>()))
                .InRequestScope();

            kernel.Bind<UserManager<ApplicationUser>>().To<UserManager<ApplicationUser>>().InRequestScope();

            kernel.Bind<ApplicationUserManager>()
                .ToMethod(u => new ApplicationUserManager(kernel.Get<UserStore<ApplicationUser>>()));

            return kernel;
        }
    }
}
