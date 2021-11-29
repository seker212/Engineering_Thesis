using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComeX.Lib.Common.ServerCommunicationModels;

namespace ComeX.WPF.Services {
    public class SignalRChatService {
        private readonly HubConnection _connection;

        public event Action<Message> ChatMessageReceived;

        public SignalRChatService(HubConnection connection) {
            _connection = connection;
            _connection.On<Message>("RecieveChatMessage", (message) => ChatMessageReceived?.Invoke(message));
        }

        public async Task Connect() {
            await _connection.StartAsync();
        }

        public async Task SendChatMessage(Message message) {
            await _connection.SendAsync("SendChatMessage", message);
        }
    }
}
