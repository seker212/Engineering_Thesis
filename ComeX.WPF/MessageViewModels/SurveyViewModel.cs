using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using ComeX.WPF.Commands;
using ComeX.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComeX.WPF.MessageViewModels {
    public class SurveyViewModel : BaseMessageViewModel {
        private ChatViewModel _chatVM;
        public SurveyResponse Survey { get; set; }

        public string MessageAuthor {
            get {
                return Survey.Username;
            }
        }

        public string MessageDateTime { //TODO
            get {
                return "Today";
            }
        }

        public string MessageContent {
            get {
                return Survey.Question;
            }
        }

        /*
        public Dictionary<SurveyAnswerResponse, int> SurveyAnswers {
            get {
                return Survey.AnswerList;
            }
        }
        */

        public Dictionary<string, int> SurveyAnswers {
            get {
                Dictionary<string, int> answers = new Dictionary<string, int>();
                foreach(var answer in Survey.AnswerList) {
                    answers.Add(answer.Key.Content, answer.Value);
                }
                return answers;
            }
        }
        

        public bool SurveyIsMultipleChoice {
            get {
                return Survey.IsMultipleChoice;
            }
        }

        public ICommand SendSurveyVoteCommand { get; }

        public SurveyViewModel(SurveyResponse survey, ChatViewModel chatVM) {
            SendSurveyVoteCommand = new SendSurveyVoteCommand(this, chatVM);

            _chatVM = chatVM;
            Survey = survey;
        }
    }
}
