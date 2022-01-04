using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class ChooseReactionCommand : ICommand {
        private readonly ChatViewModel _chatViewModel;
        private readonly ReactionViewModel _reactionViewModel;
        public ChooseReactionCommand(ChatViewModel chatViewModel, ReactionViewModel reactionViewModel) {
            _chatViewModel = chatViewModel;
            _reactionViewModel = reactionViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                ReactionMessage newReaction = new ReactionMessage();
                newReaction.Emoji = parameter.ToString();
                _reactionViewModel.Reaction = newReaction;
                _reactionViewModel.Visibility = Visibility.Hidden;
            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to add reaction.";
            }
        }
    }
}
