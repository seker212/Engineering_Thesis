using ComeX.Lib.Common.ServerCommunicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ComeX.WPF.ViewModels {
    public class SurveyViewModel : BaseMessageViewModel {
        public Survey Survey { get; set; }

        public string MessageAuthor {
            get {
                return Survey.Token;
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

        public List<SurveyAnswer> SurveyAnswers {
            get {
                return Survey.AnswerList;
            }
        }

        public List<string> SurveyAnswersContent {
            get {
                return SurveyAnswers.Select(a => a.Content).ToList();
            }
        }

        public bool SurveyIsMultipleChoice {
            get {
                return Survey.IsMultipleChoice;
            }
        }

        public SurveyViewModel(Survey survey) {
            Survey = survey;
            SurveyAnswersVisibility = System.Windows.Visibility.Visible;
        }
    }
}
