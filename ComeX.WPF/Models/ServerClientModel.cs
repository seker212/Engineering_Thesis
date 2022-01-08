using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.Models {
    public class ServerClientModel {
        public string Name;
        public string Url;
        public HubConnection Connection;
        public ChatService Service;
        public List<RoomClientModel> RoomList;

        private ChatViewModel _chatViewModel;

        public ServerClientModel(string url, ChatViewModel chatViewModel) {
            // http://localhost:5000/ComeXLogin
            Url = url;
            _chatViewModel = chatViewModel;
        }

        public void LoginToServer(LoginDataModel loginDM) {
            Connection = new HubConnectionBuilder()
                .WithUrl(Url + "/ComeXLogin")
                .Build();

            Service = new ChatService(Connection);

            try {
                Service.Connect().ContinueWith(task => {
                    if (task.Exception != null) {
                        //ErrorMessage = "Unable to connect to chat server";
                    }
                });

                Service.SendLoginMessage(new LoginMessage(loginDM.Token)).ContinueWith(task => {
                    if (task.Exception != null) {
                        //ErrorMessage = "Unable to send login message to chat server";
                    }
                });
            } catch (Exception e) {

            }
        }

        public void InitMethods() {
            Service.ChatMessageReceived += ChatService_ChatMessageReceived;
            Service.SurveyReceived += ChatService_SurveyReceived;
            Service.RoomsListReceived += ChatService_RoomsListReceived;

            Service.SpecificMessageReceived += ChatService_SpecificMessageReceived;
            Service.ChatHistoryReceived += ChatService_ChatHistoryReceived;
            Service.SurveyHistoryReceived += ChatService_SurveyHistoryReceived;
            // _connection.On<Guid>("Vote_duplicate", (surveyId) => SurveyVoteDuplicateReceived?.Invoke(surveyId));
            Service.UpdatedSurveyReceived += ChatService_UpdatedSurveyReceived;
            // _connection.On<LoadChatResponse>("Send_search", (searchChat) => SearchMessageReceived?.Invoke(searchChat));
            // _connection.On<Guid>("React_duplicate", (messageId) => MessageReactionDuplicateReceived?.Invoke(messageId));
        }

        // todo
        private void ChatService_ChatMessageReceived(MessageResponse message) {
            // AddMessage(message);
        }

        // fix
        private void ChatService_SurveyReceived(SurveyResponse survey) {
            //CurrentRoomMessages.Add(new SurveyViewModel(survey, this));
        }

        // todo
        private void ChatService_RoomsListReceived(RoomsListResponse response) {
            foreach (var room in response.RoomsList) {

                //Rooms.Add(new RoomClientModel(room.RoomId, room.Name, room.IsArchived));
            }
        }

        private void ChatService_ChatHistoryReceived(LoadChatResponse response) {
            RoomClientModel room = GetRoomById(response.RoomId);
            room.AddMessages(response.MessageList, _chatViewModel);

        }

        private void ChatService_SurveyHistoryReceived(LoadSurveyResponse response) {
            RoomClientModel room = GetRoomById(response.RoomId);
            room.AddSurveys(response.SurveyList, _chatViewModel);

        }

        private void ChatService_SpecificMessageReceived(LoadMessageResponse response) {
            RoomClientModel room = GetRoomById(response.Message.RoomId);
            room.AddMessage(response.Message, _chatViewModel);
        }

        private void ChatService_UpdatedSurveyReceived(SurveyResponse response) {
            RoomClientModel room = GetRoomById(response.RoomId);
            room.AddSurvey(response, _chatViewModel);
        }

        public RoomClientModel GetRoomById (Guid roomId) {
            return RoomList.FirstOrDefault(o => o.RoomId == roomId);
        }
    }
}
