using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.Models;
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
        private readonly ChatModel _model;
        private readonly SignalRChatService _chatService;

        public SendChatMessageCommand(ChatModel model, SignalRChatService chatService) {
            _model = model;
            _chatService = chatService;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                Message message = new Message("Anonim", Guid.Empty, Guid.Empty, _model.Content, false);
                await _chatService.SendChatMessage(message);
                _model.ErrorMessage = string.Empty;
            } catch (Exception e) {
                _model.ErrorMessage = "Unable to send message.";
            }
        }
    }
}
