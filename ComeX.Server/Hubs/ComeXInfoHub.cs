using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXInfoHub : Hub
    {
        public async Task SendInfoMessage()
        {
            await Clients.All.SendAsync("RecieveInfoMessage");
        }
    }
}
