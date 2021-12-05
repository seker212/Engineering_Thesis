using ComeX.WPF.Commands;
using ComeX.WPF.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class MainViewModel : BaseViewModel {
        private ChatViewModel ChatViewModel { get; set; }
        private LoginViewModel LoginViewModel { get; set; }
        private RegisterViewModel RegisterViewModel { get; set; }

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView {
            get { return _currentView; }
            set {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        public MainViewModel(HubConnection connection) {
            Mediator.Subscribe("ChangeViewToRegister", ChangeViewToRegister);
            Mediator.Subscribe("ChangeViewToLogin", ChangeViewToLogin);

            ChatViewModel = ChatViewModel.CreatedConnectedModel(new SignalRChatService(connection));
            LoginViewModel = LoginViewModel.CreatedConnectedModel(new LoginService(connection));
            RegisterViewModel = RegisterViewModel.CreatedConnectedModel(new RegisterService(connection));

            CurrentView = ChatViewModel;
            // CurrentView = LoginViewModel;
        }

        private void ChangeViewToLogin(object obj) {
            CurrentView = LoginViewModel;
        }

        private void ChangeViewToRegister(object obj) {
            CurrentView = RegisterViewModel;
        }
    }
}
