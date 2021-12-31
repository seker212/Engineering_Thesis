using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.ViewModels;
using ComeX.WPF.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class SendChatMessageCommand : ICommand {
        private readonly ChatViewModel _viewModel;

        public SendChatMessageCommand(ChatViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                string content = _viewModel.Content;
                if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Empty message");
                ChatMessage message = new ChatMessage(_viewModel.LoginDM.Token, Guid.Empty, Guid.Empty, _viewModel.Content);
                
                await _viewModel.CurrentServer.Service.SendChatMessage(message);
                _viewModel.ErrorMessage = string.Empty;
                _viewModel.Content = string.Empty;
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) { // TODO handling server problems
                _viewModel.ErrorMessage = "Unable to send message.";
            }
        }
    }
}
