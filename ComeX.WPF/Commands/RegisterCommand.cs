using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class RegisterCommand : ICommand {
        private readonly RegisterViewModel _viewModel;
        private readonly RegisterService _registerService;

        public RegisterCommand(RegisterViewModel viewModel, RegisterService registerService) {
            _viewModel = viewModel;
            _registerService = registerService;
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
