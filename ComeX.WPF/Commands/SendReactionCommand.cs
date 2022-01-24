using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.WPF.MessageViewModels;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeX.WPF.Commands {
    public class SendReactionCommand : ICommand {
        private readonly ReactionViewModel _reactionViewModel;
        private readonly ChatMessageViewModel _msgViewModel;
        private readonly ChatViewModel _chatViewModel;

        public SendReactionCommand(ChatViewModel chatViewModel, ChatMessageViewModel msgViewModel, ReactionViewModel reactionViewModel) {
            _reactionViewModel = reactionViewModel;
            _msgViewModel = msgViewModel;
            _chatViewModel = chatViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                if (_reactionViewModel.ChosenReaction != string.Empty) {
                    ReactionMessage voteMessage = new ReactionMessage();
                    voteMessage.Token = _chatViewModel.LoginDM.Token;
                    voteMessage.MessageId = _msgViewModel.Id;
                    voteMessage.Emoji = _reactionViewModel.ChosenReaction;

                    // List<SurveyAnswerResponse> checkedAnswers = _surveyViewModel.GetCheckedAnswers();
                    // voteMessage.AnswerId = checkedAnswers.Select(o => o.AnswerId).ToList();

                    await _chatViewModel.CurrentServer.Service.AddReaction(voteMessage);

                }
            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to send vote.";
            }
        }
    }
}
