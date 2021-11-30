using ComeX.Lib.Common.ServerCommunicationModels;
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

        public ObservableCollection<ChatMessageViewModel> Messages { get; }

        public ICommand SendChatMessageCommand { get; }

        public ChatViewModel(SignalRChatService chatService) {
            SendChatMessageCommand = new SendChatMessageCommand(this, chatService);

            Messages = new ObservableCollection<ChatMessageViewModel>();

            chatService.ChatMessageReceived += ChatService_ChatMessageReceived;
        }

        public static ChatViewModel CreatedConnectedModel(SignalRChatService chatService) {
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
    }
}
