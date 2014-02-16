using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNet.SignalR;
using Rebus;
using System.Threading;

namespace MessageService
{
    public class NotificationMessageHandler : IHandleMessages<NotificationMessage>
    {
        public IBus Bus { get; set; }
        public void Handle(NotificationMessage message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hub.Clients.All.Send(message.Message);
        }
    }
}
