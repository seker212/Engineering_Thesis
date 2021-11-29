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

namespace ComeX.WPF.Models {
    public class ChatModel : BaseModel {
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

        public ObservableCollection<ChatMessageModel> Messages { get; }

        public ICommand SendChatMessageCommand { get; }

        public ChatModel(SignalRChatService chatService) {
            SendChatMessageCommand = new SendChatMessageCommand(this, chatService);

            Messages = new ObservableCollection<ChatMessageModel>();

            chatService.ChatMessageReceived += ChatService_ChatMessageReceived;
        }

        public static ChatModel CreatedConnectedModel(SignalRChatService chatService) {
            ChatModel viewModel = new ChatModel(chatService);

            chatService.Connect().ContinueWith(task => {
                if (task.Exception != null) {
                    viewModel.ErrorMessage = "Unable to connect to color chat hub";
                }
            });

            return viewModel;
        }

        private void ChatService_ChatMessageReceived(Message message) {
            Messages.Add(new ChatMessageModel(message));
        }
    }
}
