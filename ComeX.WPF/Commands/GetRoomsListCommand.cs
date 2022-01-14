using ComeX.Lib.Common.ServerCommunicationModels;
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
        private readonly ServerViewModel _serverViewModel;

        public GetRoomsListCommand(ChatViewModel viewModel, ServerViewModel serverViewModel) {
            _viewModel = viewModel;
            _serverViewModel = serverViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                await _serverViewModel.Service.SentRoomRequest(new RoomRequest(_viewModel.LoginDM.Token));
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) { // TODO handling server problems
                _viewModel.ErrorMessage = "Unable to get rooms.";
            }
        }
    }
}
