using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Telerik.Examples.Mvc.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(object sender, string message)
        {
            await Clients.Others.SendAsync("broadcastMessage", sender, message);
        }

        public async Task SendTyping(object sender)
        {
            await Clients.Others.SendAsync("typing", sender);
        }
    }
}
