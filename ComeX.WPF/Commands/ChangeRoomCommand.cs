using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.Services;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class ChangeRoomCommand : ICommand {
        private readonly ChatViewModel _chatViewModel;
        private readonly RoomViewModel _roomViewModel;

        public ChangeRoomCommand(ChatViewModel chatViewModel, RoomViewModel roomViewModel) {
            _chatViewModel = chatViewModel;
            _roomViewModel = roomViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                _chatViewModel.CurrentRoom = _roomViewModel;

                if (_roomViewModel.MessageList.Count == 0) {
                    await _chatViewModel.CurrentServer.Service.LoadAllHistory(new LoadChatRequest(_chatViewModel.LoginDM.Token, _roomViewModel.RoomId, DateTime.Now));
                }
                _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoom));
                _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoomMessages));
            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to change room.";
            }
        }
    }
}
