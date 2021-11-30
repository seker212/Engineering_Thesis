using ComeX.WPF.Commands;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class LoginViewModel : BaseViewModel {
        private string _username;
        public string Username {
            get {
                return _username;
            }
            set {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password {
            get {
                return _password;
            }
            set {
                _password = value;
                OnPropertyChanged(nameof(Password));
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

        public ICommand LoginCommand { get; }

        public LoginViewModel(LoginService loginService) {
            LoginCommand = new LoginCommand(this, loginService);

            //Messages = new ObservableCollection<ChatMessageViewModel>();

            //chatService.ChatMessageReceived += ChatService_ChatMessageReceived;
        }

        public static LoginViewModel CreatedConnectedModel(LoginService loginService) {
            LoginViewModel viewModel = new LoginViewModel(loginService);

            loginService.Connect().ContinueWith(task => {
                if (task.Exception != null) {
                    viewModel.ErrorMessage = "Unable to connect to chat server";
                }
            });

            return viewModel;
        }

        //private void ChatService_ChatMessageReceived(Message message) {
        //    Messages.Add(new ChatMessageViewModel(message));
        //}
    }
}
