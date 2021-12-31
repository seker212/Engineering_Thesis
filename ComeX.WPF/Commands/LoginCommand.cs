using ComeX.Lib.Common.UserDatabaseAPI;
using ComeX.WPF.Models;
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
    public class LoginCommand : ICommand {
        private readonly LoginViewModel _viewModel;
        private readonly LoginService _loginService;

        public LoginCommand(LoginViewModel viewModel, LoginService loginService) {
            _viewModel = viewModel;
            _loginService = loginService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _viewModel.UnsetUsernameErrorMessage();
                _viewModel.UnsetPasswordErrorMessage();

                bool isInputCorrect = true;
                string login = _viewModel.Username;
                SecureString password = _viewModel.Password;

                if (string.IsNullOrWhiteSpace(login)) {
                    _viewModel.SetUsernameErrorMessage("Username cannot be empty");
                    isInputCorrect = false;
                }
                if (password == null || password.Length == 0) {
                    _viewModel.SetPasswordErrorMessage("Password cannot be empty");
                    isInputCorrect = false;
                }
                if (isInputCorrect) {
                    LoginDataModel loginDataModel = await _loginService.Login(login, _viewModel.SecureStringToString(password));
                    IEnumerable<ServerDataModel> serverDataModels = await _loginService.GetServers(login);
                    _viewModel.LoginDM = loginDataModel;
                    foreach (var serverDM in serverDataModels)
                        _viewModel.ServerDMs.Add(serverDM);
                    _viewModel.SetLoginDMCommand.Execute(null);
                    _viewModel.ChangeViewToChatCommand.Execute(null);
                   // _viewModel.ErrorMessage = string.Empty;
                }
            } catch (ArgumentException e) {
                _viewModel.SetUsernameErrorMessage(string.Empty);
                _viewModel.SetPasswordErrorMessage("Username or password is invalid");
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Login failed";
            }
        }
    }
}
