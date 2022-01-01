using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Commands;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.Models;
using ComeX.WPF.Services;
using ComeX.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;

namespace ComeX.WPF.ViewModels {
    public class ChatViewModel : BaseViewModel {
        private LoginDataModel _loginDM;
        public LoginDataModel LoginDM {
            get {
                return _loginDM;
            }
            set {
                _loginDM = value;
                OnPropertyChanged(nameof(LoginDM));
            }
        }

        private List<ServerDataModel> _serverDMs;
        public List<ServerDataModel> ServerDMs {
            get {
                return _serverDMs;
            }
            set {
                _serverDMs = value;
                OnPropertyChanged(nameof(ServerDMs));
            }
        }

        private ObservableCollection<ServerClientModel> _servers;
        public ObservableCollection<ServerClientModel> Servers {
            get {
                return _servers;
            }
            set {
                _servers = value;
                OnPropertyChanged(nameof(Servers));
                OnPropertyChanged(nameof(ServersNames));
            }
        }

        private ServerClientModel _currentServer;
        public ServerClientModel CurrentServer {
            get {
                return _currentServer;
            }
            set {
                _currentServer = value;
            }
        }

        public ObservableCollection<string> ServersNames {
            get {
                ObservableCollection<string> names = new ObservableCollection<string>();
                foreach (var server in Servers)
                    names.Add(server.Name);
                return names;
            }
        }

        private List<RoomClientModel> _rooms;
        public List<RoomClientModel> Rooms {
            get {
                return _rooms;
            }
            set {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private RoomClientModel _currentRoom;
        public RoomClientModel CurrentRoom {
            get {
                return _currentRoom;
            }
            set {
                _currentRoom = value;
            }
        }

        private string _currentRoomName;
        public string CurrentRoomName {
            get {
                return _currentRoomName;
            }
            set {
                _currentRoomName = value;
                OnPropertyChanged(nameof(CurrentRoomName));
            }
        }

        public List<string> RoomsNames {
            get {
                List<string> names = new List<string>();
                // todo
                /*
                foreach (var room in CurrentServer.RoomList)
                    names.Add(room.Name);
                */
                return names;
            }
        }

        private ObservableCollection<BaseMessageViewModel> _currentRoomMessages;
        public ObservableCollection<BaseMessageViewModel> CurrentRoomMessages {
            get {
                return _currentRoomMessages;
            }
            set {
                _currentRoomMessages = value;
                OnPropertyChanged(nameof(CurrentRoomMessages));
            }
        }

        private string _author;
        public string Author {
            get {
                return _author;
            }
            set {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        private string _content;
        public string Content {
            get {
                return _content;
            }
            set {
                _content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private string _date;
        public string Date {
            get {
                return _date;
            }
            set {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private Guid _replyParentId;
        public Guid ReplyParentId {
            get {
                return _replyParentId;
            }
            set {
                _replyParentId = value;
                if (value == Guid.Empty) {
                    ReplyParentAuthor = string.Empty;
                    ReplyParentContent = string.Empty;
                    ReplyParentVisibility = Visibility.Collapsed;
                }
                OnPropertyChanged(nameof(ReplyParentId));
            }
        }

        private string _replyParentAuthor;
        public string ReplyParentAuthor {
            get {
                return _replyParentAuthor;
            }
            set {
                _replyParentAuthor = value;
                OnPropertyChanged(nameof(ReplyParentAuthor));
            }
        }

        private string _replyParentContent;
        public string ReplyParentContent {
            get {
                return _replyParentContent;
            }
            set {
                _replyParentContent = value;
                OnPropertyChanged(nameof(ReplyParentContent));
                OnPropertyChanged(nameof(ReplyParentContentPrint));
            }
        }
        public string ReplyParentContentPrint {
            get {
                if (ReplyParentContent == null || ReplyParentContent.Length < 21) return ReplyParentContent;
                else return ReplyParentContent.Substring(0, 20) + "...";
            }
        }

        private Visibility _replyParentVisibility;
        public Visibility ReplyParentVisibility {
            get {
                return _replyParentVisibility;
            }
            set {
                _replyParentVisibility = value;
                OnPropertyChanged(nameof(ReplyParentVisibility));
            }
        }

        public int MessageMaxLen {
            get {
                return Consts.MESSAGE_MAXLEN;
            }
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage {
            get {
                return _errorMessage;
            }
            set {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        private bool _isConnected;
        public bool IsConnected {
            get {
                return _isConnected;
            }
            set {
                _isConnected = value;
                OnPropertyChanged(nameof(IsConnected));
            }
        }

        public ICommand SendChatMessageCommand { get; }
        public ICommand CreateSurveyCommand { get; }
        public ICommand GetServersListCommand { get; }
        public ICommand GetRoomsListCommand { get; }
        public ICommand ChangeRoomCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand UnsetReplyCommand { get; }

        private ICommand _changeViewToLoginCommand;
        public ICommand ChangeViewToLoginCommand {
            get {
                return _changeViewToLoginCommand ?? (_changeViewToLoginCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToLogin", "");
                }));
            }
        }

        public ChatViewModel(LoginService loginService, LoginDataModel loginDM, List<ServerDataModel> serverDMs) {
            ReplyParentVisibility = Visibility.Collapsed;

            ServerDMs = new List<ServerDataModel>();
            Servers = new ObservableCollection<ServerClientModel>();

            if (loginDM != null) {
                LoginDM = loginDM;

                if (serverDMs != null) {
                    ServerDMs = serverDMs;

                    foreach (var serverDM in ServerDMs) {
                        ServerClientModel newServer = new ServerClientModel(serverDM.Url);
                        newServer.Name = serverDM.Name;
                        Servers.Add(newServer);
                    }
                }
            }

            ConnectToServers();

            SendChatMessageCommand = new SendChatMessageCommand(this);
            CreateSurveyCommand = new CreateSurveyCommand(this);
            GetRoomsListCommand = new GetRoomsListCommand(this);
            GetServersListCommand = new GetServersListCommand(this, loginService);
            // ChangeRoomCommand = new ChangeRoomCommand(this, chatService);        // TODO
            OpenSettingsCommand = new OpenSettingsCommand(this, loginService);
            UnsetReplyCommand = new UnsetReplyCommand(this);

            Mediator.Subscribe("ChangeRoom", ChangeRoom);

            foreach (var server in Servers)
                GetRooms(server);

            if (Servers.Count > 0)
                CurrentServer = Servers.First();

            /*
            CurrentRoom = RoomsMessages.First();
            CurrentRoomName = CurrentRoom.Key.Name;
            CurrentRoomMessages = CurrentRoom.Value;
            */

            CurrentRoomMessages = new ObservableCollection<BaseMessageViewModel>();

            Guid g = Guid.NewGuid();
            MessageResponse m1 = new MessageResponse(g, "Anonim", "Today", Guid.Empty, Guid.Empty, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", new Dictionary<string, int>());
            MessageResponse m2 = new MessageResponse(Guid.NewGuid(), "Anonim2", "Today", Guid.Empty, Guid.Empty, "Test message 123", new Dictionary<string, int>());
            MessageResponse m3 = new MessageResponse(Guid.NewGuid(), "Anonim3", "Today", Guid.Empty, g, "Elo Witam", new Dictionary<string, int>());

            AddMessage(m1);
            AddMessage(m2);
            AddMessage(m3);

            // todo load recent messages
        }

        // fix - add not to CurrentRoomMessages
        public void AddMessage (MessageResponse msg) {
            ChatMessageViewModel newMsgVM;
            if (msg.ParentId != Guid.Empty) {
                MessageResponse parentMsg = new MessageResponse();
                foreach (var msgVM in CurrentRoomMessages) {
                    if (msgVM.GetType()==typeof(ChatMessageViewModel) && ((ChatMessageViewModel)msgVM).Message.Id == msg.ParentId) {
                        parentMsg = ((ChatMessageViewModel)msgVM).Message;
                    }
                }
                // if parentMsg not found - get parentMsg by msg.parentId from server
                newMsgVM = new ChatMessageViewModel(msg, this, parentMsg);
            } else newMsgVM = new ChatMessageViewModel(msg, this);
            CurrentRoomMessages.Add(newMsgVM);
        } 

        public static ChatViewModel CreatedConnectedModel(LoginService loginService, LoginDataModel loginDM, List<ServerDataModel> serverDMs) {
            ChatViewModel viewModel = new ChatViewModel(loginService, loginDM, serverDMs);
            return viewModel;
        }

        public void ConnectToServers() {
            try {
                foreach (var server in Servers) {
                    server.LoginToServer(LoginDM);
                    server.Service.ChatMessageReceived += ChatService_ChatMessageReceived;
                    server.Service.SurveyReceived += ChatService_SurveyReceived;
                    server.Service.RoomsListReceived += ChatService_RoomsListReceived;
                }
            } catch (Exception e) {
                ErrorMessage = "Could not connect to server";
            }
        }

        //todo
        private async void GetRooms(ServerClientModel server) {
            await server.Service.GetRoomsList();

            /*
            RoomsMessages = new Dictionary<RoomClientModel, ObservableCollection<BaseMessageViewModel>>();
            RoomsMessages.Add(new RoomClientModel(Guid.Empty, "Room1", false), new ObservableCollection<BaseMessageViewModel>());
            RoomsMessages.Add(new RoomClientModel(Guid.Empty, "Room2", false), new ObservableCollection<BaseMessageViewModel>());
            RoomsMessages.Add(new RoomClientModel(Guid.Empty, "Room3", false), new ObservableCollection<BaseMessageViewModel>());
            */
        }

        private void ChangeRoom(object obj) {
            CurrentRoom = Rooms.First(o => o.Name == (string)obj);
            CurrentRoomName = (string)obj;
            CurrentRoomMessages = new ObservableCollection<BaseMessageViewModel>();
            //CurrentRoomMessages = CurrentRoom.Value;
        }

        private void ChatService_ChatMessageReceived(MessageResponse message) {
            AddMessage(message);
        }

        // fix
        private void ChatService_SurveyReceived(SurveyResponse survey) {
            CurrentRoomMessages.Add(new SurveyViewModel(survey));
        }

        // todo
        private void ChatService_RoomsListReceived(RoomsListResponse response) {
            foreach (var room in response.RoomsList) {

                Rooms.Add(new RoomClientModel(room.RoomId, room.Name, room.IsArchived));
            }
        }
    }
}
