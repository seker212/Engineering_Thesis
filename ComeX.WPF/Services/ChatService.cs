using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComeX.Lib.Common.ServerCommunicationModels;

namespace ComeX.WPF.Services {
    public class ChatService {
        private readonly HubConnection _connection;

        public event Action<Message> ChatMessageReceived;
        public event Action<Survey> SurveyReceived;

        public ChatService(HubConnection connection) {
            _connection = connection;
            _connection.On<Message>("RecieveChatMessage", (message) => ChatMessageReceived?.Invoke(message));
            _connection.On<Survey>("RecieveChatSurvey", (survey) => SurveyReceived?.Invoke(survey));
        }

        public async Task Connect() {
            await _connection.StartAsync();
        }

        public async Task SendChatMessage(Message message) {
            await _connection.SendAsync("SendChatMessage", message);
        }

        public async Task SendChatSurvey(Survey survey) {
            await _connection.SendAsync("SendChatSurvey", survey);
        }
    }
}
