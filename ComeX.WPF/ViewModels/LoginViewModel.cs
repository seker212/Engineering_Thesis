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

        private ICommand _changeViewToRegisterCommand;
        public ICommand ChangeViewToRegisterCommand { get {
                return _changeViewToRegisterCommand ?? (_changeViewToRegisterCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToRegister", "");
                }));
            } }

        private Brush _defaultBorderBrush;

        public LoginViewModel(LoginService loginService) {
            LoginCommand = new LoginCommand(this, loginService);

            UsernameErrorVisibility = Visibility.Collapsed;
            PasswordErrorVisibility = Visibility.Collapsed;

            _defaultBorderBrush = (Brush)Application.Current.MainWindow.FindResource("DarkBlue3");

            UsernameBoxBorder = _defaultBorderBrush;
            PasswordBoxBorder = _defaultBorderBrush;
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

        public void SetUsernameErrorMessage (string errorMessage) {
            UsernameError = errorMessage;
            UsernameErrorVisibility = Visibility.Visible;
            UsernameBoxBorder = Brushes.Red;
        }
        public void UnsetUsernameErrorMessage() {
            UsernameErrorVisibility = Visibility.Collapsed;
            UsernameBoxBorder = _defaultBorderBrush;
        }

        public void SetPasswordErrorMessage(string errorMessage) {
            PasswordError = errorMessage;
            PasswordErrorVisibility = Visibility.Visible;
            PasswordBoxBorder = Brushes.Red;
        }

        public void UnsetPasswordErrorMessage() {
            PasswordErrorVisibility = Visibility.Collapsed;
            PasswordBoxBorder = _defaultBorderBrush;
        }
    }
}
