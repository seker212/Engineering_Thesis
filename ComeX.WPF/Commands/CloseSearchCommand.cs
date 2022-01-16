using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class CloseSearchCommand : ICommand {
        private readonly ChatViewModel _viewModel;

        public CloseSearchCommand(ChatViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _viewModel.SearchVisibility = Visibility.Collapsed;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to unset reply.";
            }
        }
    }
}
