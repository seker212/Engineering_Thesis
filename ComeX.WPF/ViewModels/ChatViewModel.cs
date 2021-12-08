using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Commands;
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

        public ObservableCollection<BaseMessageViewModel> Messages { get; }

        public ICommand SendChatMessageCommand { get; }
        public ICommand CreateSurveyCommand { get; }

        public ChatViewModel(ChatService chatService) {
            SendChatMessageCommand = new SendChatMessageCommand(this, chatService);
            CreateSurveyCommand = new CreateSurveyCommand(this, chatService);

            Messages = new ObservableCollection<BaseMessageViewModel>();

            chatService.ChatMessageReceived += ChatService_ChatMessageReceived;
            chatService.SurveyReceived += ChatService_SurveyReceived;
        }

        public static ChatViewModel CreatedConnectedModel(ChatService chatService) {
            ChatViewModel viewModel = new ChatViewModel(chatService);

            chatService.Connect().ContinueWith(task => {
                if (task.Exception != null) {
                    viewModel.ErrorMessage = "Unable to connect to chat server";
                }
            });

            return viewModel;
        }

        private void ChatService_ChatMessageReceived(Message message) {
            Messages.Add(new ChatMessageViewModel(message));
        }

        private void ChatService_SurveyReceived(Survey survey) {
            Messages.Add(new SurveyViewModel(survey));
        }
    }
}
