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
        public event Action<LoadMessageResponse> SpecificMessageReceived;
        public event Action<LoadChatResponse> ChatHistoryReceived;
        public event Action<LoadSurveyResponse> SurveyHistoryReceived;
        public event Action<Guid> SurveyVoteDuplicateReceived;
        public event Action<SurveyResponse> UpdatedSurveyReceived;
        public event Action<LoadChatResponse> SearchMessageReceived;
        public event Action<Guid> MessageReactionDuplicateReceived;

        public ChatService(HubConnection connection) {
            _connection = connection;
            _connection.On<MessageResponse>("Message_created", (message) => ChatMessageReceived?.Invoke(message));
            _connection.On<SurveyResponse>("Survey_created", (survey) => SurveyReceived?.Invoke(survey));
            _connection.On<RoomsListResponse>("Sending_rooms", (roomsList) => RoomsListReceived?.Invoke(roomsList));
            _connection.On<LoadMessageResponse>("Load_message", (message) => SpecificMessageReceived?.Invoke(message));
            _connection.On<LoadChatResponse>("Send_chat_history", (chatHistory) => ChatHistoryReceived?.Invoke(chatHistory));
            _connection.On<LoadSurveyResponse>("Send_survey_history", (surveyHistory) => SurveyHistoryReceived?.Invoke(surveyHistory));
            _connection.On<Guid>("Vote_duplicate", (surveyId) => SurveyVoteDuplicateReceived?.Invoke(surveyId));
            _connection.On<SurveyResponse>("Survey_updated", (survey) => UpdatedSurveyReceived?.Invoke(survey));
            _connection.On<LoadChatResponse>("Send_search", (searchChat) => SearchMessageReceived?.Invoke(searchChat));
            _connection.On<Guid>("React_duplicate", (messageId) => MessageReactionDuplicateReceived?.Invoke(messageId));
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

        public async Task SentRoomRequest(RoomRequest request) {
            await _connection.SendAsync("SentRoomRequest", request);
        }

        public async Task LoadSpecificMessage(LoadMessageRequest request) {
            await _connection.SendAsync("LoadSpecificMessage", request);
        }

        public async Task LoadChatHistory(LoadChatRequest request) {
            await _connection.SendAsync("LoadChatHistory", request);
        }

        public async Task LoadSurveyHistory(LoadSurveyRequest request) {
            await _connection.SendAsync("LoadSurveyHistory", request);
        }

        public async Task SearchMessage(SearchMessageRequest request) {
            await _connection.SendAsync("SearchMessage", request);
        }

        public async Task AddReaction(ReactionMessage request) {
            await _connection.SendAsync("AddReaction", request);
        }
    }
}
