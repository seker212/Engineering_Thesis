using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Commands;
using ComeX.WPF.Models;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class ServerViewModel {
        private string _name;
        public string Name {
            get {
                return _name;
            }
            set {
                _name = value;
            }
        }
        public string Url;
        public HubConnection Connection;
        public ChatService Service;
        public List<RoomViewModel> RoomList;
        public List<RoomViewModel> ArchivedRoomList;

        private ChatViewModel _chatViewModel;

        public ICommand ChangeServerCommand { get; }
        public ICommand GetRoomsListCommand { get; }

        public ServerViewModel(string url, string name, ChatViewModel chatViewModel) {
            // http://localhost:5000/ComeXLogin
            Url = url;
            Name = name;
            RoomList = new List<RoomViewModel>();
            ArchivedRoomList = new List<RoomViewModel>();
            _chatViewModel = chatViewModel;

            ChangeServerCommand = new ChangeServerCommand(chatViewModel, this);
            GetRoomsListCommand = new GetRoomsListCommand(chatViewModel, this);

            LoginToServer(chatViewModel.LoginDM);
            InitMethods();
            GetRoomsList();
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

        public void GetRoomsList() {
            GetRoomsListCommand.Execute(null); 
        }

        public void InitMethods() {
            Service.ChatMessageReceived += ChatService_ChatMessageReceived;
            Service.SurveyVoteReceived += ChatService_SurveyVoteReceived;
            Service.RoomsListReceived += ChatService_RoomsListReceived;

            Service.SpecificMessageReceived += ChatService_SpecificMessageReceived;
            Service.ChatHistoryReceived += ChatService_ChatHistoryReceived;
            Service.SurveyHistoryReceived += ChatService_SurveyHistoryReceived;
            Service.AllHistoryReceived += ChatService_AllHistoryReceived;
            // _connection.On<Guid>("Vote_duplicate", (surveyId) => SurveyVoteDuplicateReceived?.Invoke(surveyId));
            Service.UpdatedSurveyReceived += ChatService_UpdatedSurveyReceived;
            Service.UpdatedMessageReceived += ChatService_UpdatedMessageReceived;
            Service.SearchMessageReceived += ChatService_SearchMessageReceived;
            // _connection.On<Guid>("React_duplicate", (messageId) => MessageReactionDuplicateReceived?.Invoke(messageId));
        }

        private void ChatService_ChatMessageReceived(MessageResponse message) {
            RoomViewModel room = GetRoomById(message.RoomId);
            room.AddMessage(message, _chatViewModel);
        }

        private void ChatService_SurveyVoteReceived(SurveyVoteResponse surveyVote) {
            RoomViewModel room = GetRoomById(surveyVote.Survey.RoomId);
            room.AddSurveyVote(surveyVote, _chatViewModel);
        }

        private void ChatService_RoomsListReceived(RoomsListResponse response) {
            foreach (var room in response.RoomsList) {
                if (room.IsArchived)
                    ArchivedRoomList.Add(new RoomViewModel(_chatViewModel, this, room.RoomId, room.Name, room.IsArchived));
                else
                    RoomList.Add(new RoomViewModel(_chatViewModel, this, room.RoomId, room.Name, room.IsArchived));
            }
        }

        private void ChatService_ChatHistoryReceived(LoadChatResponse response) {
            RoomViewModel room = GetRoomById(response.RoomId);
            room.AddMessages(response.MessageList, _chatViewModel);
        }

        private void ChatService_SurveyHistoryReceived(LoadSurveyVoteResponse response) {
            RoomViewModel room = GetRoomById(response.RoomId);
            room.AddSurveysVote(response.SurveyVoteList, _chatViewModel);
        }

        private void ChatService_AllHistoryReceived(LoadAllResponse response) {
            RoomViewModel room = GetRoomById(response.RoomId);
            room.AddMessagesAndSurveys(response.MessageList, response.SurveyVoted, _chatViewModel);
        }
        private void ChatService_SpecificMessageReceived(LoadMessageResponse response) {
            RoomViewModel room = GetRoomById(response.Message.RoomId);
            room.UpdateParentMessage(response.Message, _chatViewModel);
            _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoomMessages));
        }

        private void ChatService_UpdatedSurveyReceived(SurveyResponse response) {
            RoomViewModel room = GetRoomById(response.RoomId);
            room.AddSurvey(response, _chatViewModel);
        }

        private void ChatService_UpdatedMessageReceived(LoadMessageResponse response) {
            RoomViewModel room = GetRoomById(response.Message.RoomId);
            room.AddMessage(response.Message, _chatViewModel);
            // _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoomMessages));
        }

        private void ChatService_SearchMessageReceived(LoadChatResponse response) {
            foreach (var msg in response.MessageList) {
                _chatViewModel.AddSearchMessage(msg);
            }
            _chatViewModel.SearchVisibility = Visibility.Visible;
            _chatViewModel.SearchPhraseLabel = _chatViewModel.SearchPhrase;
            int elo = response.MessageList.Count;
            _chatViewModel.SearchPhraseNumberLabel = elo.ToString();
            _chatViewModel.SearchPhraseRoom = _chatViewModel.CurrentRoom.Name;
        }

        public RoomViewModel GetRoomById (Guid roomId) {
            var result = RoomList.FirstOrDefault(o => o.RoomId == roomId);
            if (result == null)
                result = ArchivedRoomList.FirstOrDefault(o => o.RoomId == roomId);
            return result;
        }
    }
}
