using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Commands;
using ComeX.WPF.Models;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        private Dictionary<RoomClientModel, ObservableCollection<BaseMessageViewModel>> _roomsMessages;
        public Dictionary<RoomClientModel, ObservableCollection<BaseMessageViewModel>> RoomsMessages {
            get {
                return _roomsMessages;
            }
            set {
                _roomsMessages = value;
                OnPropertyChanged(nameof(RoomsMessages));
            }
        }

        public List<string> RoomsNames {
            get {
                return RoomsMessages.Keys.Select(o => o.Name).ToList();
            }
        }

        private KeyValuePair<RoomClientModel, ObservableCollection<BaseMessageViewModel>> _currentRoom;
        public KeyValuePair<RoomClientModel, ObservableCollection<BaseMessageViewModel>> CurrentRoom {
            get {
                return _currentRoom;
            }
            set {
                _currentRoom = value;
                OnPropertyChanged(nameof(CurrentRoom));
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
        public ICommand GetRoomsListCommand { get; }
        public ICommand ChangeRoomCommand { get; }
        public ICommand OpenSettingsCommand { get; }

        private ICommand _changeViewToLoginCommand;
        public ICommand ChangeViewToLoginCommand {
            get {
                return _changeViewToLoginCommand ?? (_changeViewToLoginCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToLogin", "");
                }));
            }
        }

        public ChatViewModel(ChatService chatService, LoginService loginService) {
            SendChatMessageCommand = new SendChatMessageCommand(this, chatService);
            CreateSurveyCommand = new CreateSurveyCommand(this, chatService);
            GetRoomsListCommand = new GetRoomsListCommand(this, chatService);
            ChangeRoomCommand = new ChangeRoomCommand(this, chatService);
            OpenSettingsCommand = new OpenSettingsCommand(this, loginService);

            Mediator.Subscribe("ChangeRoom", ChangeRoom);

            chatService.ChatMessageReceived += ChatService_ChatMessageReceived;
            chatService.SurveyReceived += ChatService_SurveyReceived;
            chatService.RoomsListReceived += ChatService_RoomsListReceived;

            GetRooms();

            CurrentRoom = RoomsMessages.First();
            CurrentRoomName = CurrentRoom.Key.Name;
            CurrentRoomMessages = CurrentRoom.Value;
            //CurrentRoomsMessages = new ObservableCollection<BaseMessageViewModel>();
        }

        public static ChatViewModel CreatedConnectedModel(ChatService chatService, LoginService loginService) {
            ChatViewModel viewModel = new ChatViewModel(chatService, loginService);

            chatService.Connect().ContinueWith(task => {
                if (task.Exception != null) {
                    viewModel.ErrorMessage = "Unable to connect to chat server";
                }
            });

            return viewModel;
        }

        private void ChangeRoom(object obj) {
            CurrentRoom = RoomsMessages.First(o => o.Key.Name == (string)obj);
            CurrentRoomName = (string)obj;
            CurrentRoomMessages = new ObservableCollection<BaseMessageViewModel>();
            //CurrentRoomMessages = CurrentRoom.Value;
        }

        //todo
        private void GetRooms() {
            RoomsMessages = new Dictionary<RoomClientModel, ObservableCollection<BaseMessageViewModel>>();
            RoomsMessages.Add(new RoomClientModel(Guid.Empty, "Room1", false), new ObservableCollection<BaseMessageViewModel>());
            RoomsMessages.Add(new RoomClientModel(Guid.Empty, "Room2", false), new ObservableCollection<BaseMessageViewModel>());
            RoomsMessages.Add(new RoomClientModel(Guid.Empty, "Room3", false), new ObservableCollection<BaseMessageViewModel>());
        }

        private void ChatService_ChatMessageReceived(Message message) {
            CurrentRoomMessages.Add(new ChatMessageViewModel(message));
        }

        private void ChatService_SurveyReceived(Survey survey) {
            CurrentRoomMessages.Add(new SurveyViewModel(survey));
        }

        private void ChatService_RoomsListReceived(RoomsListResponse response) {
            foreach (var room in response.RoomsList) {
                RoomsMessages.Add(new RoomClientModel(room.RoomId, room.Name, room.IsArchived),
                    new ObservableCollection<BaseMessageViewModel>());
            }
        }
    }
}
