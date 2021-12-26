using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands.Settings {
    public class ChangePasswordCommand : ICommand {
        private readonly SettingsViewModel _viewModel;
        private readonly LoginService _service;

        public ChangePasswordCommand(SettingsViewModel viewModel, LoginService loginService) {
            _viewModel = viewModel;
            _service = loginService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _viewModel.UnsetChangePasswordErrorMessage();

                bool isInputCorrect = true;
                string oldPassword = _viewModel.OldPasswordValue;
                string newPassword = _viewModel.NewPasswordValue;

                if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword)) {
                    _viewModel.SetChangePasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                }
                if (oldPassword == newPassword) {
                    _viewModel.SetChangePasswordErrorMessage("New password cannot be same as the old one");
                    isInputCorrect = false;
                }
                if (!IsPasswordFormatValid()) {
                    _viewModel.SetChangePasswordErrorMessage("Password must be at least 8 characters long, contain at least one lower letter, one upper letter and a digit");
                    isInputCorrect = false;
                }
                if (isInputCorrect) {
                    bool result = await _service.ChangePassword(_viewModel.LoginDM.Username, oldPassword, newPassword);
                    if (result) {
                        _viewModel.SetChangePasswordErrorMessage("Password changed");
                    } else {
                        _viewModel.SetChangePasswordErrorMessage("Couldn't change the password");
                    }
                }
            } catch (ArgumentException e) {
                _viewModel.SetChangePasswordErrorMessage("Couldn't change password");
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Operation failed";
            }
        }

        private bool IsPasswordFormatValid() {
            return (_viewModel.NewPasswordValue.Length > 7
                && _viewModel.NewPasswordValue.Any(char.IsDigit)
                && _viewModel.NewPasswordValue.Any(char.IsLower)
                && _viewModel.NewPasswordValue.Any(char.IsUpper));
        }
    }
}
