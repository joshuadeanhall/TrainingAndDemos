using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageService.Infrastructure;
using Microsoft.AspNet.SignalR;
using Microsoft.ServiceBus.Messaging;

namespace MessageService
{
    public class MessageHub : Hub
    {
        public static ConnectionMapping<string> Connections =
            new ConnectionMapping<string>();

        public void SendMessage(string message)
        {
            Clients.All.Send(message);
        }

        public override Task OnConnected()
        {
            string name = Context.QueryString["username"];

            Connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            string name = Context.QueryString["username"];

            Connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            string name = Context.QueryString["username"];

            if (!Connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                Connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

    }
}
