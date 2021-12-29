using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class GetRoomsListCommand : ICommand {
        private readonly ChatViewModel _viewModel;

        public GetRoomsListCommand(ChatViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                await _viewModel.CurrentServer.Service.GetRoomsList();
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) { // TODO handling server problems
                _viewModel.ErrorMessage = "Unable to get rooms.";
            }
        }
    }
}
