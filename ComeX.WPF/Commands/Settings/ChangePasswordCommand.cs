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

                // todo
                /*
                if (string.IsNullOrWhiteSpace(login)) {
                    _viewModel.SetUsernameErrorMessage("Username cannot be empty");
                    isInputCorrect = false;
                }
                if (password == null || password.Length == 0) {
                    _viewModel.SetPasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                } */
                if (isInputCorrect) {
                    bool result = await _service.ChangePassword(_viewModel.LoginDM.Username, _viewModel.OldPasswordValue, _viewModel.NewPasswordValue);
                    if (result) {
                        _viewModel.SetChangePasswordErrorMessage("Password changed");
                    } else {
                        _viewModel.SetChangePasswordErrorMessage("Couldn't change the password");
                    }
                }
                // todo nie wiem czy te exceptiony potrzebne
            } catch (ArgumentException e) {
                _viewModel.SetChangePasswordErrorMessage("");
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Login failed";
            }
        }
    }
}
