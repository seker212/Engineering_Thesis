using ComeX.WPF.Commands;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Security;
using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Models;

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

        private SecureString _password;
        public SecureString Password {
            get {
                return _password;
            }
            set {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _usernameError;
        public string UsernameError {
            get {
                return _usernameError;
            }
            set {
                _usernameError = value;
                OnPropertyChanged(nameof(UsernameError));
            }
        }

        private string _passwordError;
        public string PasswordError {
            get {
                return _passwordError;
            }
            set {
                _passwordError = value;
                OnPropertyChanged(nameof(PasswordError));
            }
        }

        private Visibility _usernameErrorVisibility;
        public Visibility UsernameErrorVisibility {
            get {
                return _usernameErrorVisibility;
            }
            set {
                _usernameErrorVisibility = value;
                OnPropertyChanged(nameof(UsernameErrorVisibility));
            }
        }

        private Visibility _passwordErrorVisibility;
        public Visibility PasswordErrorVisibility {
            get {
                return _passwordErrorVisibility;
            }
            set {
                _passwordErrorVisibility = value;
                OnPropertyChanged(nameof(PasswordErrorVisibility));
            }
        }

        private Visibility _loadingVisibility;
        public Visibility LoadingVisibility {
            get {
                return _loadingVisibility;
            }
            set {
                _loadingVisibility = value;
                OnPropertyChanged(nameof(LoadingVisibility));
            }
        }

        private Brush _usernameBoxBorder;
        public Brush UsernameBoxBorder {
            get {
                return _usernameBoxBorder;
            }
            set {
                _usernameBoxBorder = value;
                OnPropertyChanged(nameof(UsernameBoxBorder));
            }
        }

        private Brush _passwordBoxBorder;
        public Brush PasswordBoxBorder {
            get {
                return _passwordBoxBorder;
            }
            set {
                _passwordBoxBorder = value;
                OnPropertyChanged(nameof(PasswordBoxBorder));
            }
        }

        public int UsernameMaxLen {
            get {
                return Consts.USERNAME_MAXLEN;
            }
        }

        public int PasswordMaxLen {
            get {
                return Consts.PASSWORD_MAXLEN;
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

        public ICommand LoginCommand { get; }

        private ICommand _changeViewToRegisterCommand;
        public ICommand ChangeViewToRegisterCommand { get {
                return _changeViewToRegisterCommand ?? (_changeViewToRegisterCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToRegister", "");
                }));
            } }

        private ICommand _changeViewToChatCommand;
        public ICommand ChangeViewToChatCommand {
            get {
                return _changeViewToChatCommand ?? (_changeViewToChatCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToChat", "");
                }));
            }
        }

        private ICommand _setLoginDMCommand;
        public ICommand SetLoginDMCommand {
            get {
                return _setLoginDMCommand ?? (_setLoginDMCommand = new RelayCommand(x => {
                    Mediator.Notify("SetLoginDM", LoginDM);
                }));
            }
        }

        private ICommand _setServerDMCommand;
        public ICommand SetServernDMCommand {
            get {
                return _setServerDMCommand ?? (_setServerDMCommand = new RelayCommand(x => {
                    Mediator.Notify("SetServerDM", ServerDMs);
                }));
            }
        }

        private Brush _defaultBorderBrush;

        public LoginViewModel(LoginService loginService) {
            ServerDMs = new List<ServerDataModel>();
            LoginCommand = new LoginCommand(this, loginService);

            UsernameErrorVisibility = Visibility.Collapsed;
            PasswordErrorVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Hidden;

            _defaultBorderBrush = (Brush)Application.Current.MainWindow.FindResource("DarkBlue3");

            UsernameBoxBorder = _defaultBorderBrush;
            PasswordBoxBorder = _defaultBorderBrush;
        }

        public static LoginViewModel CreatedConnectedModel(LoginService loginService) {
            LoginViewModel viewModel = new LoginViewModel(loginService);

            return viewModel;
        }

        public void SetUsernameErrorMessage (string errorMessage) {
            if (errorMessage != string.Empty) {
                UsernameError = errorMessage;
                UsernameErrorVisibility = Visibility.Visible;
            }
            UsernameBoxBorder = Brushes.Red;
        }

        public void UnsetUsernameErrorMessage() {
            UsernameErrorVisibility = Visibility.Collapsed;
            UsernameBoxBorder = _defaultBorderBrush;
        }

        public void SetPasswordErrorMessage(string errorMessage) {
            if (errorMessage != string.Empty) {
                PasswordError = errorMessage;
                PasswordErrorVisibility = Visibility.Visible;
            }
            PasswordBoxBorder = Brushes.Red;
        }

        public void UnsetPasswordErrorMessage() {
            PasswordErrorVisibility = Visibility.Collapsed;
            PasswordBoxBorder = _defaultBorderBrush;
        }
    }
}
