using KO_Angular_Demo.Models;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Driver;
using Ninject;
using Ninject.Modules;

namespace KO_Angular_Demo.Infrastructure.Ninject
{
    public class UserStoreModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<UserStore<ApplicationUser>>()
                .ToMethod(c => new UserStore<ApplicationUser>(Kernel.Get<MongoDatabase>()))
                .InTransientScope();
            
            Kernel.Bind<UserManager<ApplicationUser>>().To<UserManager<ApplicationUser>>().InTransientScope();
        }
    }
}