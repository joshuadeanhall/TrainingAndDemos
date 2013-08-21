using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using TestWeb.Messages;
using System.Configuration;
using MongoDB.Driver;

namespace TestWeb.Backend
{
    public class SendCommandHandler : IHandleMessages<SendCommandMessage>
    {
        public ILog Logger = LogManager.GetLogger(typeof (SendCommandHandler));
        public IBus Bus { get; set; }
        public void Handle(SendCommandMessage message)
        {

            var connectionString = ConfigurationManager.AppSettings.Get("MongoDatabaseUrl") ??
                                   ConfigurationManager.AppSettings.Get("MONGOHQ_URL") ??
                                   ConfigurationManager.AppSettings.Get("MONGOLAB_URI");
            var url = new MongoUrl(connectionString);
            var client = new MongoClient(url);
            var server = client.GetServer();

            var database = server.GetDatabase(url.DatabaseName);

            var collection = database.GetCollection<SampleData>("example");
            var example = new SampleData { Name = message.Name };
            collection.Insert(example);          
            Logger.Info("Starting message");
            Bus.Send(new SendCompleteMessage {Name = string.Format("Message {0} processed name is {1}", message.Id, message.Name)});
        }
    }
}
