using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNet.SignalR;
using Rebus;

namespace MessageService.Handlers
{
    public class GlobalNotificationMessageHandler :  IHandleMessages<GlobalNotificationMessage>
    {
        public IBus Bus { get; set; }
        public void Handle(GlobalNotificationMessage message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            hub.Clients.All.Send(message.Title, message.Message);
        }
    }
}
