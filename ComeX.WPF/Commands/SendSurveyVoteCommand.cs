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
    public class SendSurveyVoteCommand : ICommand {
        private readonly SurveyViewModel _surveyViewModel;
        private readonly ChatViewModel _chatViewModel;

        public SendSurveyVoteCommand(SurveyViewModel surveyViewModel, ChatViewModel chatViewModel) {
            _surveyViewModel = surveyViewModel;
            _chatViewModel = chatViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public async void Execute(object parameter) {
            try {
                SurveyVoteMessage vote = new SurveyVoteMessage();
                //vote.AnswerId = GetAnswers();

                // await _chatViewModel.CurrentServer.Service.SendVote(vote);

            } catch (ArgumentException e) {
                _chatViewModel.ErrorMessage = e.Message;
            } catch (Exception e) {
                _chatViewModel.ErrorMessage = "Unable to send vote.";
            }
        }

    }
}
