using Kendo.Mvc.UI;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string senderId, string senderName, string message)
        {
            // Broadcast the message to all clients except the sender.
            await Clients.Others.SendAsync("broadcastMessage", senderId, senderName, message);
        }

        public async Task SendTyping(string senderId, string senderName)
        {
            // Broadcast the typing notification to all clients except the sender.
            await Clients.Others.SendAsync("typing", senderId, senderName);
        }
    }
}
