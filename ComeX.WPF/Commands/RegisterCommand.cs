using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class RegisterCommand : ICommand {
        private readonly RegisterViewModel _viewModel;
        private readonly LoginService _service;

        public RegisterCommand(RegisterViewModel viewModel, LoginService service) {
            _viewModel = viewModel;
            _service = service;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _viewModel.UnsetUsernameErrorMessage();
                _viewModel.UnsetPasswordErrorMessage();
                _viewModel.UnsetRetypePasswordErrorMessage();

                bool isInputCorrect = true;
                string login = _viewModel.Username;
                string password = _viewModel.PasswordValue;
                string retypePassword = _viewModel.RetypePasswordValue;

                if (string.IsNullOrWhiteSpace(login)) {
                    _viewModel.SetUsernameErrorMessage("Username cannot be empty");
                    isInputCorrect = false;
                }
                if (string.IsNullOrEmpty(password)) {
                    _viewModel.SetPasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                }
                if (string.IsNullOrEmpty(retypePassword)) {
                    _viewModel.SetRetypePasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                }
                if (!IsPasswordFormatValid()) {
                    _viewModel.SetPasswordErrorMessage("Password must be at least 8 characters long, contain lower letter, upper letter and a digit");
                    isInputCorrect = false;
                }
                else if (!ArePasswordsEqual()) {
                    _viewModel.SetRetypePasswordErrorMessage("Passwords are not equal");
                    isInputCorrect = false;
                }
                if (isInputCorrect) {
                    bool result = await _service.Register(login, password);
                    if (result) {
                        LoginDataModel loginDataModel = await _service.Login(login, password);
                        _viewModel.LoginDM = loginDataModel;
                        _viewModel.SetLoginDMCommand.Execute(null);
                        _viewModel.ChangeViewToChatCommand.Execute(null);
                    }
                }
            } catch (ArgumentException e) {
                _viewModel.SetUsernameErrorMessage("This username is already taken");
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Register failed";
            }
        }

        private bool ArePasswordsEqual() {
            return (_viewModel.PasswordValue == _viewModel.RetypePasswordValue);
        }

        private bool IsPasswordFormatValid() {
            return (_viewModel.PasswordValue.Length > 7
                && _viewModel.PasswordValue.Any(char.IsDigit)
                && _viewModel.PasswordValue.Any(char.IsLower)
                && _viewModel.PasswordValue.Any(char.IsUpper));
        }
    }
}
