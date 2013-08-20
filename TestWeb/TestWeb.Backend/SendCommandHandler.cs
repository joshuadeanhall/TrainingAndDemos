using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using TestWeb.Data;
using TestWeb.Messages;

namespace TestWeb.Backend
{
    public class SendCommandHandler : IHandleMessages<SendCommandMessage>
    {
        public ILog Logger = LogManager.GetLogger(typeof (SendCommandHandler));
        public IBus Bus { get; set; }
        public void Handle(SendCommandMessage message)
        {
            Logger.Info("Starting message");
            Bus.Send(new SendCompleteMessage {Name = string.Format("Message {0} processed", message.Id)});
        }
    }
}
