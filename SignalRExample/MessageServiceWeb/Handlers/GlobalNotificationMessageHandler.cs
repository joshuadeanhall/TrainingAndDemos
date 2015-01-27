using Messages;
using MessageServiceWeb.Hubs;
using Microsoft.AspNet.SignalR;
using Rebus;

namespace MessageServiceWeb.Handlers
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
