using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class UnsetReplyCommand : ICommand {
        private readonly ChatViewModel _viewModel;

        public UnsetReplyCommand(ChatViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _viewModel.ReplyParentId = null;
                _viewModel.ReplyParentAuthor = string.Empty;
                _viewModel.ReplyParentContent = string.Empty;
                _viewModel.ReplyParentVisibility = Visibility.Collapsed;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to unset reply.";
            }
        }
    }
}
