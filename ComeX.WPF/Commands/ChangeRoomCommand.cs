using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class ChangeRoomCommand : ICommand {
        private readonly ChatViewModel _viewModel;
        private readonly ChatService _service;

        public ChangeRoomCommand(ChatViewModel viewModel, ChatService service) {
            _viewModel = viewModel;
            _service = service;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            try {
                _viewModel.CurrentRoom = _viewModel.RoomsMessages.First();
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _viewModel.ErrorMessage = "Unable to change room.";
            }
        }
    }
}
