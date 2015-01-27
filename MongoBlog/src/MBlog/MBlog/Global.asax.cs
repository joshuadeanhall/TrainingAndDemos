using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MBlog.Domain;
using MBlog.Infrastructure.Automapper;
using MBlog.Infrastructure.CastleWindsor;
using MBlog.Services;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MBlog
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
           var container = SetupContainer();
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            InitalizeDatabase(container);
            AutoMapperConfiguration.Configure();
            PostAppHost postApp = new PostAppHost();
            postApp.Init();
            postApp.Container.Adapter = new WindsorContainerAdapter(container);
            //(new PostAppHost()).Init();
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
            InitializeSettings(database);

        }

        private void InitializeSettings(MongoDatabase database)
        {
            var collection = database.GetCollection<Setting>("settings");
            var emailSetting = collection.FindOne(Query.EQ("Name", "Email"));
            if (emailSetting == null)
            {
                emailSetting = new Setting {Name = "Email", Value = "localhost@localhost.net"};
                collection.Insert(emailSetting);
            }
            var aboutMeSetting = collection.FindOne(Query.EQ("Name", "About Me"));
            if (aboutMeSetting == null)
            {
                aboutMeSetting = new Setting {Name = "About Me", Value = ""};
                collection.Insert(aboutMeSetting);
            }
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