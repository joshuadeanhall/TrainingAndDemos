using System.Configuration;
using KO_Angular_Demo.Models;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Driver;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(KO_Angular_Demo.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(KO_Angular_Demo.App_Start.NinjectWebCommon), "Stop")]

namespace KO_Angular_Demo.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
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

        }        
    }
}
