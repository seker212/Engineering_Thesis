using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.ViewModels;
using ComeX.WPF.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class ReactionCommand : ICommand {
        private readonly ChatMessageViewModel _msgViewModel;
        private readonly ChatViewModel _chatViewModel;
        private ReactionWindow _reactionView;

        public ReactionCommand(ChatMessageViewModel msgViewModel, ChatViewModel chatViewModel) {
            _msgViewModel = msgViewModel;
            _chatViewModel = chatViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                ReactionMessage newReaction = OpenReactionWindow();
                if (newReaction != null) {
                    newReaction.Token = _chatViewModel.LoginDM.Token;
                    newReaction.MessageId = _msgViewModel.Message.Id;

                    // await _chatViewModel.CurrentServer.Service.SendChatSurvey(newReaction);
                    _chatViewModel.ErrorMessage = string.Empty;
                }
            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to add reaction.";
            }
        }

        private ReactionMessage OpenReactionWindow() {
            ReactionMessage newReaction;

            _reactionView = new ReactionWindow();
            _reactionView.DataContext = new ReactionViewModel(_chatViewModel);
            _reactionView.Owner = Application.Current.MainWindow;
            _reactionView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Application.Current.MainWindow.IsEnabled = false;

            _reactionView.ShowDialog();
            newReaction = _reactionView.Reaction;

            return newReaction;
        }
    }
}
