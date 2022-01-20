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

        private ObservableCollection<ServerViewModel> _servers;
        public ObservableCollection<ServerViewModel> Servers {
            get {
                return _servers;
            }
            set {
                _servers = value;
                OnPropertyChanged(nameof(Servers));
            }
        }

        private ServerViewModel _currentServer;
        public ServerViewModel CurrentServer {
            get {
                return _currentServer;
            }
            set {
                _currentServer = value;
                Rooms = _currentServer.RoomList;
                ArchivedRooms = _currentServer.ArchivedRoomList;
                CurrentRoom = null;
                OnPropertyChanged(nameof(CurrentServer));
                OnPropertyChanged(nameof(Rooms));
            }
        }

        private List<RoomViewModel> _rooms;
        public List<RoomViewModel> Rooms {
            get {
                return _rooms;
            }
            set {
                _rooms = value;
                CurrentRoom = null;
                OnPropertyChanged(nameof(Rooms));
                OnPropertyChanged(nameof(CurrentRoom));
            }
        }

        private List<RoomViewModel> _archivedRooms;
        public List<RoomViewModel> ArchivedRooms {
            get {
                return _archivedRooms;
            }
            set {
                _archivedRooms = value;
                if (_archivedRooms != null && _archivedRooms.Count > 0)
                    ArchivedRoomsVisibility = Visibility.Visible;
                else {
                    ArchivedRoomsVisibility = Visibility.Collapsed;
                }
                OnPropertyChanged(nameof(ArchivedRooms));
            }
        }

        private Visibility _archivedRoomsVisibility;
        public Visibility ArchivedRoomsVisibility {
            get {
                return _archivedRoomsVisibility;
            }
            set {
                _archivedRoomsVisibility = value;
                OnPropertyChanged(nameof(ArchivedRoomsVisibility));
            }
        }

        private RoomViewModel _currentRoom;
        public RoomViewModel CurrentRoom {
            get {
                return _currentRoom;
            }
            set {
                _currentRoom = value;
                if (_currentRoom != null && !_currentRoom.IsArchived) {
                    SendMessageEnabled = true;
                } else {
                    SendMessageEnabled = false;
                }
                OnPropertyChanged(nameof(CurrentRoom));
                OnPropertyChanged(nameof(CurrentRoomName));
                OnPropertyChanged(nameof(CurrentRoomMessages));
                OnPropertyChanged(nameof(SendMessageEnabled));
            }
        }

        public string CurrentRoomName {
            get {
                return CurrentRoom == null ? string.Empty : CurrentRoom.Name;
            }
        }

        private LoadingViewModel _loadingVM;
        public LoadingViewModel LoadingVM {
            get {
                return _loadingVM;
            }
            set {
                _loadingVM = value;
            }
        }

        public ObservableCollection<BaseMessageViewModel> CurrentRoomMessages {
            get {
                if (CurrentRoom == null) return new ObservableCollection<BaseMessageViewModel>();
                else {
                    ObservableCollection<BaseMessageViewModel> list = new ObservableCollection<BaseMessageViewModel>();
                    list.Add(new LoadHistoryViewModel(this));
                    list.Add(LoadingVM);
                    foreach (var el in CurrentRoom.MessageList)
                        list.Add(el);
                    return list;
                }
            }
        }

        private ObservableCollection<ChatMessageViewModel> _searchMessages;
        public ObservableCollection<ChatMessageViewModel> SearchMessages {
            get {
                return _searchMessages;
            }
            set {
                _searchMessages = value;
                OnPropertyChanged(nameof(SearchMessages));
            }
        }

        private string _searchPhrase;
        public string SearchPhrase {
            get {
                return _searchPhrase;
            }
            set {
                _searchPhrase = value;
                OnPropertyChanged(nameof(SearchPhrase));
            }
        }

        private string _searchPhraseNumberLabel;
        public string SearchPhraseNumberLabel {
            get {
                return _searchPhraseNumberLabel;
            }
            set {
                _searchPhraseNumberLabel = value;
                OnPropertyChanged(nameof(SearchPhraseNumberLabel));
            }
        }

        private string _searchPhraseLabel;
        public string SearchPhraseLabel {
            get {
                return _searchPhraseLabel;
            }
            set {
                _searchPhraseLabel = value;
                OnPropertyChanged(nameof(SearchPhraseLabel));
            }
        }

        private string _searchPhraseRoom;
        public string SearchPhraseRoom {
            get {
                return _searchPhraseRoom;
            }
            set {
                _searchPhraseRoom = value;
                OnPropertyChanged(nameof(SearchPhraseRoom));
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

        private Nullable<Guid> _replyParentId;
        public Nullable<Guid> ReplyParentId {
            get {
                return _replyParentId;
            }
            set {
                _replyParentId = value;
                if (value == null) {
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

        private Visibility _searchVisibility;
        public Visibility SearchVisibility {
            get {
                return _searchVisibility;
            }
            set {
                _searchVisibility = value;
                OnPropertyChanged(nameof(SearchVisibility));
            }
        }

        public int MessageMaxLen {
            get {
                return Consts.MESSAGE_MAXLEN;
            }
        }

        public int SearchMaxLen {
            get {
                return Consts.SEARCH_MAXLEN;
            }
        }

        private bool _sendMessageEnabled;
        public bool SendMessageEnabled {
            get {
                return _sendMessageEnabled;
            }
            set {
                _sendMessageEnabled = value;
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
        public ICommand OpenSettingsCommand { get; }
        public ICommand UnsetReplyCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand CloseSearchCommand { get; }

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
            ArchivedRoomsVisibility = Visibility.Collapsed;
            SearchVisibility = Visibility.Collapsed;
            LoadingVM = new LoadingViewModel();
            SendMessageEnabled = false;

            ServerDMs = new List<ServerDataModel>();
            Servers = new ObservableCollection<ServerViewModel>();

            if (loginDM != null) {
                LoginDM = loginDM;

                if (serverDMs != null) {
                    ServerDMs = serverDMs;

                    foreach (var serverDM in ServerDMs) {
                        ServerViewModel newServer = new ServerViewModel(serverDM.Url, serverDM.Name, this);
                        Servers.Add(newServer);
                    }
                }
            }

            SendChatMessageCommand = new SendChatMessageCommand(this);
            CreateSurveyCommand = new CreateSurveyCommand(this);
            GetServersListCommand = new GetServersListCommand(this, loginService);
            OpenSettingsCommand = new OpenSettingsCommand(this, loginService);
            UnsetReplyCommand = new UnsetReplyCommand(this);
            SearchCommand = new SearchCommand(this);
            CloseSearchCommand = new CloseSearchCommand(this);
        }

        public void AddSearchMessage(MessageResponse msg) {
            ChatMessageViewModel newMsgVM;
            if (msg.ParentId != null) {
                MessageResponse parentMsg = new MessageResponse();
                foreach (var msgVM in SearchMessages) {
                    if (msgVM.Message.Id == msg.ParentId) {
                        parentMsg = msgVM.Message;
                    }
                }
                // if parentMsg not found - get parentMsg by msg.parentId from server
                newMsgVM = new ChatMessageViewModel(msg, this, true);
            } else newMsgVM = new ChatMessageViewModel(msg, this, true);
            SearchMessages.Add(newMsgVM);
        }

            public static ChatViewModel CreatedConnectedModel(LoginService loginService, LoginDataModel loginDM, List<ServerDataModel> serverDMs) {
            ChatViewModel viewModel = new ChatViewModel(loginService, loginDM, serverDMs);
            return viewModel;
        }
    }
}
