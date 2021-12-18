using ComeX.Lib.Common.UserDatabaseAPI;
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
            ChatViewModel = ChatViewModel.CreatedConnectedModel(new ChatService(connection));
            LoginViewModel = LoginViewModel.CreatedConnectedModel(new LoginService(connection));
            RegisterViewModel = RegisterViewModel.CreatedConnectedModel(new LoginService(connection));

            Mediator.Subscribe("ChangeViewToRegister", ChangeViewToRegister);
            Mediator.Subscribe("ChangeViewToLogin", ChangeViewToLogin);
            Mediator.Subscribe("ChangeViewToChat", ChangeViewToChat);
            Mediator.Subscribe("SetLoginDM", SetLoginDM);

            // CurrentView = ChatViewModel;
            CurrentView = LoginViewModel;
        }

        private void ChangeViewToLogin(object obj) {
            LoginViewModel.ResetViewModel();
            CurrentView = LoginViewModel;
        }

        private void ChangeViewToRegister(object obj) {
            RegisterViewModel.ResetViewModel();
            CurrentView = RegisterViewModel;
        }

        private void ChangeViewToChat(object obj) {
            CurrentView = ChatViewModel;
        }

        private void SetLoginDM(object obj) {
            ChatViewModel.LoginDM = (LoginDataModel)obj;
        }
    }
}
