using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands.Settings {
    public class DeleteAccountCommand : ICommand {
        private readonly SettingsViewModel _viewModel;
        private readonly LoginService _service;

        public DeleteAccountCommand(SettingsViewModel viewModel, LoginService loginService) {
            _viewModel = viewModel;
            _service = loginService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                string password = _viewModel.DeletePasswordValue;
                if (string.IsNullOrEmpty(password)) {
                    _viewModel.SetDeleteAccountErrorMessage("Password cannot be empty");
                } else {
                    bool result = await _service.DeleteAccount(_viewModel.LoginDM.Username, password);
                    if (result) {
                        _viewModel.LogoutCommand.Execute(null);
                        _viewModel.ErrorMessage = "";
                        ((Window)parameter).Close();
                    } else {
                        _viewModel.SetDeleteAccountErrorMessage("Couldn't delete the account. Ensure that the password is correct");
                        _viewModel.ErrorMessage = "Operation failed";
                    }
                }
            } catch (ArgumentException e) {
                _viewModel.SetDeleteAccountErrorMessage("Couldn't delete account");
                _viewModel.ErrorMessage = "Operation failed";
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Operation failed";
            }
        }
    }
}
