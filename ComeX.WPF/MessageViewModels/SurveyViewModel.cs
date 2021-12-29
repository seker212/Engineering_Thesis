using ComeX.Lib.Common.ServerCommunicationModels;
using ComeX.Lib.Common.ServerResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ComeX.WPF.MessageViewModels {
    public class SurveyViewModel : BaseMessageViewModel {
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

        public Dictionary<SurveyAnswerResponse, int> SurveyAnswers {
            get {
                return Survey.AnswerList;
            }
        }

        /*
        public Dictionary<SurveyAnswerResponse, int> SurveyAnswers {
            get {
                return Survey.AnswerList;
            }
        }
        */

        /*
        public List<string> SurveyAnswersContent {
            get {
                return SurveyAnswers.Select(a => a.Content).ToList();
            }
        }
        */

        public bool SurveyIsMultipleChoice {
            get {
                return Survey.IsMultipleChoice;
            }
        }

        public SurveyViewModel(SurveyResponse survey) {
            Survey = survey;
            SurveyAnswersVisibility = System.Windows.Visibility.Visible;
        }
    }
}
