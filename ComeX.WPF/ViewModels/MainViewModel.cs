using ComeX.WPF.Commands;
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

        private ICommand _changeViewToLogin { get; set; }
        private ICommand _changeViewToRegister { get; set; }

        public MainViewModel(ChatViewModel chatViewModel) {
            ChatViewModel = chatViewModel;
        }

        public MainViewModel(LoginViewModel loginViewModel) {
            LoginViewModel = loginViewModel;
            CurrentView = LoginViewModel;
        }

        public ICommand ChangeViewToLoginCommand {
            get {
                return _changeViewToLogin ?? (_changeViewToLogin = new RelayCommand(
                   x => {
                       ChangeViewToLogin();
                   }));
            }
        }

        public ICommand ChangeViewToRegisterCommand {
            get {
                return _changeViewToRegister ?? (_changeViewToRegister = new RelayCommand(
                   x => {
                       ChangeViewToRegister();
                   }));
            }
        }

        private void ChangeViewToLogin() {
            CurrentView = LoginViewModel;
        }

        private void ChangeViewToRegister() {
            CurrentView = RegisterViewModel;
        }
    }
}
