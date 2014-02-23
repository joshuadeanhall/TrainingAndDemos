using Messages;
using MessageServiceWeb.Hubs;
using Microsoft.AspNet.SignalR;
using Rebus;

namespace MessageServiceWeb.Handlers
{
    public class NotificationMessageHandler : IHandleMessages<NotificationMessage>
    {
        public IBus Bus { get; set; }
        public void Handle(NotificationMessage message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            foreach (var id in MessageHub.Connections.GetConnections(message.UserName))
            {
                hub.Clients.Client(id).Send(message.Title, message.Message);
            }
        }
    }
}
