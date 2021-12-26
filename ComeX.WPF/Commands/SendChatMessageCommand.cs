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
        private readonly ChatService _chatService;

        public SendChatMessageCommand(ChatViewModel viewModel, ChatService chatService) {
            _viewModel = viewModel;
            _chatService = chatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                string content = _viewModel.Content;
                if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Empty message");
                Message message = new Message(_viewModel.LoginDM.Username, Guid.Empty, Guid.Empty, _viewModel.Content, false);
                await _chatService.SendChatMessage(message);
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
