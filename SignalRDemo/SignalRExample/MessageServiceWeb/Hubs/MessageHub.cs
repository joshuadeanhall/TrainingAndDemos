using System.Linq;
using System.Threading.Tasks;
using MessageServiceWeb.Infrastructure;
using Microsoft.AspNet.SignalR;

namespace MessageServiceWeb.Hubs
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
