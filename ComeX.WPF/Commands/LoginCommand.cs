using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                //Login login = new Login("Anonim", Guid.Empty, Guid.Empty, _viewModel.Content, false);
                //await _chatService.SendChatMessage(message);
                //_viewModel.ErrorMessage = string.Empty;
                //_viewModel.Content = string.Empty;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to send message.";
            }
        }
    }
}
