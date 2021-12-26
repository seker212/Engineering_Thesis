using ComeX.Lib.Auth;
using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Server.DAL;
using ComeX.Server.DatabaseModels;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXHub : Hub
    {
        ILoginManager _loginManager;
        IConnectionCache _connectionCache;
        UserRepository usrRepo;

        public ComeXHub(ILoginManager loginManager, IConnectionCache connectionCache)
        {
            _loginManager = loginManager;
            _connectionCache = connectionCache;
            usrRepo = new UserRepository("Server = 127.0.0.1; Port = 5432; Database = postgres; User Id = postgres; Password = mysecretpassword;");
        }

        public async Task SendLoginMessage(LoginMessage msg)
        {
            //Klient wysyła mi tu token i ja sobie pobieram dane o nim
            _loginManager.Login(Context.ConnectionId, msg.Token);
            Guid usrId = Guid.Parse(_connectionCache[msg.Token].UserId);
            string usrName = _connectionCache[msg.Token].Username;
            try
            {
                User loginUser = usrRepo.GetUser(Guid.Parse(_connectionCache[msg.Token].UserId));
                await Clients.Caller.SendAsync("Logged in");
            } catch(Exception e)
            {
                User loginUser = usrRepo.InsertUser(new User(usrId, usrName));
                await Clients.Caller.SendAsync("First login");
            }
        }

        //zapytanie o pokoje
        public async Task SentRoomRequest()
        {
            await Clients.All.SendAsync("RecieveRoomRequest");
        }

        // otrzymano message
        public async Task SendChatMessage(ChatMessage message)
        {
            //bez pliku
            await Clients.All.SendAsync("RecieveChatMessage", message);
        }

        // otrzymano ankietę
        public async Task SendChatSurvey()
        {
            await Clients.All.SendAsync("RecieveChatSurvey");
        }

        // otrzymano odpowiedzi do ankiety
        public async Task SendChatSurveyAnswer()
        {
            await Clients.All.SendAsync("RecieveChatSurveyAnswer");
        }

        // otrzymano głos do ankiety
        public async Task SendChatSurveyVote()
        {
            await Clients.All.SendAsync("RecieveChatSurveyVote");
        }
    }
}
