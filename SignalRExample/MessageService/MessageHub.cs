using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus.Messaging;

namespace MessageService
{
    public class MessageHub : Hub
    {
        public void SendMessage(string message)
        {
            Clients.All.Send(message);
        }

    }
}
