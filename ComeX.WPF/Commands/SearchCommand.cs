using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class SearchCommand : ICommand {
        private readonly ChatViewModel _viewModel;

        public SearchCommand(ChatViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                string content = _viewModel.SearchPhrase;
                if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Empty message");
                _viewModel.SearchMessages = new ObservableCollection<MessageViewModels.ChatMessageViewModel>();

                SearchMessageRequest request = new SearchMessageRequest(_viewModel.LoginDM.Token, _viewModel.CurrentRoom.RoomId, content);
                await _viewModel.CurrentServer.Service.SearchMessage(request);
                _viewModel.ErrorMessage = string.Empty;
            } catch (ArgumentException e) {
                _viewModel.ErrorMessage = e.Message;
            } catch (Exception e) { // TODO handling server problems
                _viewModel.ErrorMessage = "Unable to send message.";
            }
        }
    }
}
