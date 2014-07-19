using System.Configuration;
using MongoDB.Driver;
using Ninject.Modules;

namespace KO_Angular_Demo.Infrastructure.Ninject
{
    public class MongoModule : NinjectModule
    {
        public override void Load()
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MongoDatabaseUrl") ??
                       ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                       ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            //TODO pull blog name from app.config
            Kernel.Bind<MongoDatabase>().ToConstant(server.GetDatabase(url.DatabaseName));
        }
    }
}