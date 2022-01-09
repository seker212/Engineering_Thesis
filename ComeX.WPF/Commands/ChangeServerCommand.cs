using ComeX.WPF.Models;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class ChangeServerCommand : ICommand {
        private readonly ChatViewModel _chatViewModel;
        private readonly ServerViewModel _serverViewModel;

        public ChangeServerCommand(ChatViewModel chatViewModel, ServerViewModel serverViewModel) {
            _chatViewModel = chatViewModel;
            _serverViewModel = serverViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _chatViewModel.CurrentServer = _serverViewModel;

            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) { // TODO handling server problems
                _chatViewModel.ErrorMessage = "Unable to change server.";
            }
        }
    }
}
