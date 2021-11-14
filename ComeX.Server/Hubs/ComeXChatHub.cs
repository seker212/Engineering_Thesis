using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXChatHub : Hub
    {
        public async Task SendChatMessage()
        {
            await Clients.All.SendAsync("RecieveChatMessage");
        }
    }
}
