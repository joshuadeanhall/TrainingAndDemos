using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MongoDB.Driver;

namespace MBlog.Infrastructure.Installers
{
    public class MongoInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var connectionstring = ConfigurationManager.AppSettings.Get("MongoDatabaseUrl") ??
                                   ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                                   ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionstring);
            var client = new MongoClient(url);
            var server = client.GetServer();
            //TODO pull blog name from app.config
            container.Register(Component.For<MongoDatabase>()
                .Instance(server.GetDatabase(url.DatabaseName))
                .LifestylePerWebRequest());
        }
    }
}