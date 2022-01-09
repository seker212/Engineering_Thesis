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

        private RoomViewModel _currentRoom;
        public RoomViewModel CurrentRoom {
            get {
                return _currentRoom;
            }
            set {
                _currentRoom = value;
                if (value == null) {
                    CurrentRoomMessages = new ObservableCollection<BaseMessageViewModel>();
                } else {
                    CurrentRoomMessages = _currentRoom.MessageList;
                    SendMessageEnabled = true;
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
        public ICommand ChangeRoomCommand { get; }
        public ICommand OpenSettingsCommand { get; }
        public ICommand UnsetReplyCommand { get; }
        public ICommand SearchCommand { get; }

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
        }

        // fix - add not to CurrentRoomMessages
        public void AddMessage (MessageResponse msg) {
            ChatMessageViewModel newMsgVM;
            if (msg.ParentId != null) {
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
                newMsgVM = new ChatMessageViewModel(msg, this, parentMsg);
            } else newMsgVM = new ChatMessageViewModel(msg, this);
            SearchMessages.Add(newMsgVM);
        }

        // temp
        public void AddSurvey(SurveyMessage survey) {
            SurveyResponse newSurvey = new SurveyResponse();
            newSurvey.Username = "Anonim4";
            newSurvey.SendTime = "Today";
            newSurvey.RoomId = Guid.Empty;
            newSurvey.Question = survey.Question;
            newSurvey.IsMultipleChoice = survey.IsMultipleChoice;
            Dictionary<SurveyAnswerResponse, int> answers = new Dictionary<SurveyAnswerResponse, int>();
            foreach (var answer in survey.AnswerList) {
                answers.Add(new SurveyAnswerResponse(Guid.NewGuid(), answer), 0);
            }
            newSurvey.AnswerList = answers;
            CurrentRoomMessages.Add(new SurveyViewModel(newSurvey, this));
        }

            public static ChatViewModel CreatedConnectedModel(LoginService loginService, LoginDataModel loginDM, List<ServerDataModel> serverDMs) {
            ChatViewModel viewModel = new ChatViewModel(loginService, loginDM, serverDMs);
            return viewModel;
        }
    }
}
