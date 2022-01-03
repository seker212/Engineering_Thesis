using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;

namespace ComeX.WPF.Services {
    public class ChatService {
        private readonly HubConnection _connection;

        public event Action<MessageResponse> ChatMessageReceived;
        public event Action<SurveyResponse> SurveyReceived;
        public event Action<RoomsListResponse> RoomsListReceived;

        public ChatService(HubConnection connection) {
            _connection = connection;
            _connection.On<MessageResponse>("Message_created", (message) => ChatMessageReceived?.Invoke(message));
            _connection.On<SurveyResponse>("ReceiveChatSurvey", (survey) => SurveyReceived?.Invoke(survey));
            _connection.On<RoomsListResponse>("Sending_rooms", (roomsList) => RoomsListReceived?.Invoke(roomsList));
        }

        public async Task Connect() {
            await _connection.StartAsync();
        }

        public async Task SendLoginMessage(LoginMessage message) {
            await _connection.SendAsync("SendLoginMessage", message);
        }

        public async Task SendChatMessage(ChatMessage message) {
            await _connection.SendAsync("SendChatMessage", message);
        }

        public async Task SendChatSurvey(SurveyMessage survey) {
            await _connection.SendAsync("SendChatSurvey", survey);
        }

        public async Task SendChatSurveyVote(SurveyVoteMessage vote) {
            await _connection.SendAsync("SendChatSurveyVote", vote);
        }

        public async Task GetRoomsList() {
            await _connection.SendAsync("GetRoomsList");
        }
    }
}
