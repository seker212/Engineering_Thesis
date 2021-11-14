using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXLoginHub : Hub
    {
        public async Task SendLoginMessage()
        {
            await Clients.All.SendAsync("RecieveLoginMessage");
        }
    }
}
