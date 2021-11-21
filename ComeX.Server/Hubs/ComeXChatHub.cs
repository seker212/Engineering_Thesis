using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.Hubs
{
    public class ComeXChatHub : Hub
    {
        // otrzymano wiadomość tekstową
        public async Task SendChatMessage()
        {
            // check czy ma plik
            await Clients.All.SendAsync("RecieveChatMessage");
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
