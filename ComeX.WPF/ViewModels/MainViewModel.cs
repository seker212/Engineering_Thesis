using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Commands;
using ComeX.WPF.Models;
using ComeX.WPF.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class MainViewModel : BaseViewModel {
        private LoginService _loginService { get; }
        private ChatService _chatService { get; }
        private ChatViewModel _chatViewModel { get; set; }
        private LoginViewModel _loginViewModel { get; set; }
        private RegisterViewModel _registerViewModel { get; set; }

        private BaseViewModel _currentView;
        public BaseViewModel CurrentView {
            get { return _currentView; }
            set {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private ResizeMode _windowResizeMode;
        public ResizeMode WindowResizeMode {
            get { return _windowResizeMode; }
            set {
                _windowResizeMode = value;
                OnPropertyChanged("WindowResizeMode");
            }
        }

        private int _windowMinWidth;
        public int WindowMinWidth {
            get { return _windowMinWidth; }
            set {
                _windowMinWidth = value;
                OnPropertyChanged("WindowMinWidth");
            }
        }

        public MainViewModel() {
            _loginService = new LoginService();

            _loginViewModel = LoginViewModel.CreatedConnectedModel(_loginService);
            _registerViewModel = RegisterViewModel.CreatedConnectedModel(_loginService);
            _chatViewModel = ChatViewModel.CreatedConnectedModel(_loginService, null, null);

            Mediator.Subscribe("ChangeViewToRegister", ChangeViewToRegister);
            Mediator.Subscribe("ChangeViewToLogin", ChangeViewToLogin);
            Mediator.Subscribe("ChangeViewToChat", ChangeViewToChat);
            Mediator.Subscribe("SetLoginDM", SetLoginDM);
            Mediator.Subscribe("SetServerDM", SetServerDM);

            CurrentView = _loginViewModel;
        }

        private void ChangeViewToLogin(object obj) {
            _loginViewModel = LoginViewModel.CreatedConnectedModel(_loginService);
            CurrentView = _loginViewModel;
            WindowResizeMode = ResizeMode.NoResize;
            WindowMinWidth = 500;
        }

        private void ChangeViewToRegister(object obj) {
            _registerViewModel = RegisterViewModel.CreatedConnectedModel(_loginService);
            CurrentView = _registerViewModel;
            WindowResizeMode = ResizeMode.NoResize;
            WindowMinWidth = 500;
        }

        private void ChangeViewToChat(object obj) {
            if (CurrentView == _loginViewModel)
                _chatViewModel = ChatViewModel.CreatedConnectedModel(_loginService, _loginViewModel.LoginDM, _loginViewModel.ServerDMs);
            else _chatViewModel = ChatViewModel.CreatedConnectedModel(_loginService, _registerViewModel.LoginDM, null);
            //SetLoginDM(_loginViewModel.LoginDM);
            CurrentView = _chatViewModel;
            WindowResizeMode = ResizeMode.CanResize;
            WindowMinWidth = 850;
        }

        private void SetLoginDM(object obj) {
            _chatViewModel.LoginDM = (LoginDataModel)obj;
        }

        private void SetServerDM(object obj) {
            _chatViewModel.ServerDMs = (List<ServerDataModel>)obj;
        }
    }
}
