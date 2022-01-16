using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class LoadHistoryCommand : ICommand {
        private readonly ChatViewModel _chatViewModel;

        public LoadHistoryCommand(ChatViewModel chatViewModel) {
            _chatViewModel = chatViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                BaseMessageViewModel lastMsg = _chatViewModel.CurrentRoomMessages[1];
                DateTime lastMsgTime = new DateTime();
                if (lastMsg.GetType() == typeof(ChatMessageViewModel)) {
                    lastMsgTime = ((ChatMessageViewModel)lastMsg).Message.SendTime;
                } else if (lastMsg.GetType() == typeof(SurveyViewModel)) {
                    lastMsgTime = ((SurveyViewModel)lastMsg).Survey.SendTime;
                } else {
                    throw new Exception();
                }
                await _chatViewModel.CurrentServer.Service.LoadAllHistory(new LoadChatRequest(_chatViewModel.LoginDM.Token, _chatViewModel.CurrentRoom.RoomId, lastMsgTime));
                _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoom));
                _chatViewModel.OnPropertyChanged(nameof(_chatViewModel.CurrentRoomMessages));
            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to load chat history.";
            }
        }
    }
}
