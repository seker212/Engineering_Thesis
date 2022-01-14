using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class SetReplyCommand : ICommand {
        private readonly ChatMessageViewModel _msgViewModel;
        private readonly ChatViewModel _chatViewModel;

        public SetReplyCommand(ChatMessageViewModel msgViewModel, ChatViewModel chatViewModel) {
            _msgViewModel = msgViewModel;
            _chatViewModel = chatViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _chatViewModel.ReplyParentId = _msgViewModel.Message.Id;
                _chatViewModel.ReplyParentAuthor = _msgViewModel.Message.Username;
                _chatViewModel.ReplyParentContent = _msgViewModel.Message.Content;
                _chatViewModel.ReplyParentVisibility = Visibility.Visible;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to set reply.";
            }
        }
    }
}
