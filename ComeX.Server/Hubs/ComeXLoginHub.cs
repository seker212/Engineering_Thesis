using ComeX.Lib.Auth;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXLoginHub : Hub
    {
        ILoginManager _loginManager;
        IConnectionCache _connectionCache;

        public ComeXLoginHub(ILoginManager loginManager, IConnectionCache connectionCache)
        {
            _loginManager = loginManager;
            _connectionCache = connectionCache;
        }

        public async Task SendLoginMessage()
        {
            //Klient wysyła mi tu token i ja sobie pobieram dane o nim
            await Clients.All.SendAsync("RecieveLoginMessage");
        }
    }
}
