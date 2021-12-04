using ComeX.WPF.Commands;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ComeX.WPF.ViewModels {
    public class RegisterViewModel : BaseViewModel {
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

        private SecureString _retypePassword;
        public SecureString RetypePassword {
            get {
                return _retypePassword;
            }
            set {
                _retypePassword = value;
                OnPropertyChanged(nameof(RetypePassword));
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

        private string _retypePasswordError;
        public string RetypePasswordError {
            get {
                return _retypePasswordError;
            }
            set {
                _retypePasswordError = value;
                OnPropertyChanged(nameof(RetypePasswordError));
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

        private Visibility _retypePasswordErrorVisibility;
        public Visibility RetypePasswordErrorVisibility {
            get {
                return _retypePasswordErrorVisibility;
            }
            set {
                _retypePasswordErrorVisibility = value;
                OnPropertyChanged(nameof(RetypePasswordErrorVisibility));
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

        private Brush _retypePasswordBoxBorder;
        public Brush RetypePasswordBoxBorder {
            get {
                return _retypePasswordBoxBorder;
            }
            set {
                _retypePasswordBoxBorder = value;
                OnPropertyChanged(nameof(RetypePasswordBoxBorder));
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

        public ICommand RegisterCommand { get; }

        private ICommand _changeViewToLoginCommand;
        public ICommand ChangeViewToLoginCommand {
            get {
                return _changeViewToLoginCommand ?? (_changeViewToLoginCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToLogin", "");
                }));
            }
        }

        private Brush _defaultBorderBrush;

        public RegisterViewModel(RegisterService registerService) {
            RegisterCommand = new RegisterCommand(this, registerService);

            UsernameErrorVisibility = Visibility.Collapsed;
            PasswordErrorVisibility = Visibility.Collapsed;
            RetypePasswordErrorVisibility = Visibility.Collapsed;

            _defaultBorderBrush = (Brush)Application.Current.MainWindow.FindResource("DarkBlue3");

            UsernameBoxBorder = _defaultBorderBrush;
            PasswordBoxBorder = _defaultBorderBrush;
            RetypePasswordBoxBorder = _defaultBorderBrush;
        }

        public static RegisterViewModel CreatedConnectedModel(RegisterService registerService) {
            RegisterViewModel viewModel = new RegisterViewModel(registerService);

            registerService.Connect().ContinueWith(task => {
                if (task.Exception != null) {
                    viewModel.ErrorMessage = "Unable to connect to chat server";
                }
            });

            return viewModel;
        }

        public void SetUsernameErrorMessage(string errorMessage) {
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

        public void SetRetypePasswordErrorMessage(string errorMessage) {
            RetypePasswordError = errorMessage;
            RetypePasswordErrorVisibility = Visibility.Visible;
            RetypePasswordBoxBorder = Brushes.Red;
        }

        public void UnsetRetypePasswordErrorMessage() {
            RetypePasswordErrorVisibility = Visibility.Collapsed;
            RetypePasswordBoxBorder = _defaultBorderBrush;
        }
    }
}
