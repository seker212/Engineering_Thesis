using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Commands;
using ComeX.WPF.Commands.Settings;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.ViewModels {
    public class SettingsViewModel : BaseViewModel {
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

        private SecureString _oldPassword;
        public SecureString OldPassword {
            get {
                return _oldPassword;
            }
            set {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
                OldPasswordValue = SecureStringToString(value);
            }
        }

        private SecureString _newPassword;
        public SecureString NewPassword {
            get {
                return _newPassword;
            }
            set {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
                NewPasswordValue = SecureStringToString(value);
            }
        }

        private SecureString _deletePassword;
        public SecureString DeletePassword {
            get {
                return _deletePassword;
            }
            set {
                _deletePassword = value;
                OnPropertyChanged(nameof(DeletePassword));
                DeletePasswordValue = SecureStringToString(value);
            }
        }

        private string _oldPasswordValue;
        public string OldPasswordValue {
            get {
                return _oldPasswordValue;
            }
            set {
                _oldPasswordValue = value;
            }
        }

        private string _newPasswordValue;
        public string NewPasswordValue {
            get {
                return _newPasswordValue;
            }
            set {
                _newPasswordValue = value;
            }
        }

        private string _deletePasswordValue;
        public string DeletePasswordValue {
            get {
                return _deletePasswordValue;
            }
            set {
                _deletePasswordValue = value;
            }
        }

        private string _changePasswordError;
        public string ChangePasswordError {
            get {
                return _changePasswordError;
            }
            set {
                _changePasswordError = value;
                OnPropertyChanged(nameof(ChangePasswordError));
            }
        }

        private string _deleteAccountError;
        public string DeleteAccountError {
            get {
                return _deleteAccountError;
            }
            set {
                _deleteAccountError = value;
                OnPropertyChanged(nameof(DeleteAccountError));
            }
        }

        private Visibility _changePasswordErrorVisibility;
        public Visibility ChangePasswordErrorVisibility {
            get {
                return _changePasswordErrorVisibility;
            }
            set {
                _changePasswordErrorVisibility = value;
                OnPropertyChanged(nameof(ChangePasswordErrorVisibility));
            }
        }

        private Visibility _deleteAccountErrorVisibility;
        public Visibility DeleteAccountErrorVisibility {
            get {
                return _deleteAccountErrorVisibility;
            }
            set {
                _deleteAccountErrorVisibility = value;
                OnPropertyChanged(nameof(DeleteAccountErrorVisibility));
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

        private ICommand _logoutCommand;
        public ICommand LogoutCommand {
            get {
                return _logoutCommand ?? (_logoutCommand = new RelayCommand(x => {
                    Mediator.Notify("ChangeViewToLogin", "");
                }));
            }
        }

        public ICommand ChangePasswordCommand { get; }
        public ICommand DeleteAccountCommand { get; }

        public SettingsViewModel(LoginService loginService, LoginDataModel loginDM) {
            LoginDM = loginDM;
            OldPasswordValue = string.Empty;
            NewPasswordValue = string.Empty;
            DeletePasswordValue = string.Empty;

            ChangePasswordCommand = new ChangePasswordCommand(this, loginService);
            DeleteAccountCommand = new DeleteAccountCommand(this, loginService);
            
            ChangePasswordErrorVisibility = Visibility.Collapsed;
            DeleteAccountErrorVisibility = Visibility.Collapsed;
        }

        public static SettingsViewModel CreatedConnectedModel(LoginService loginService, LoginDataModel loginDM) {
            SettingsViewModel viewModel = new SettingsViewModel(loginService, loginDM);
            return viewModel;
        }

        public void SetChangePasswordErrorMessage(string errorMessage) {
            ChangePasswordError = errorMessage;
            ChangePasswordErrorVisibility = Visibility.Visible;
        }

        public void UnsetChangePasswordErrorMessage() {
            ChangePasswordErrorVisibility = Visibility.Collapsed;
        }

        public void SetDeleteAccountErrorMessage(string errorMessage) {
            DeleteAccountError = errorMessage;
            DeleteAccountErrorVisibility = Visibility.Visible;
        }

        public void UnsetDeleteAccountErrorMessage() {
            DeleteAccountErrorVisibility = Visibility.Collapsed;
        }
    }
}
