using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SimpleServices.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(string username, string message)
        {
            Clients.Others.MessageSent(string.Format("{0} sent message {1}", username, message));
        }
    }
}
