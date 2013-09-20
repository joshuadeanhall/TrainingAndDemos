using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MBlog.Infrastructure.CastleWindsor;
using MongoDB.Driver;

namespace MBlog
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
           var container = SetupContainer();
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            InitalizeDatabase(container);
            AutoMapperConfiguration.Configure();
        }

        private void InitalizeDatabase(IWindsorContainer container)
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MongoDatabaseUrl") ??
                                   ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                                   ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            var database = server.GetDatabase(url.DatabaseName);
            InitializeUser(database);
            
        }

        private void InitializeUser(MongoDatabase database)
        {
            var users = database.GetCollection<User>("users");
            if (users.Count() >= 1) return;
            var user = new User
            {
                Email = "",
                Password = "Password1",
                UserName = "Admin"
            };
            users.Insert(user);
        }

        private IWindsorContainer SetupContainer()
        {
            return new WindsorContainer()
                .Install(FromAssembly.This());
            
        }
    }
}