using ComeX.Lib.Common.ServerResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.WPF.MessageViewModels {
    public class SurveyAnswerViewModel {
        public SurveyAnswerResponse AnswerResponse { get; set; }
        public string Content {
            get {
                return AnswerResponse.Content;
            }
        }
        public int Votes { get; set; }
        public bool IsChecked { get; set; }

        public SurveyAnswerViewModel() { }
        public SurveyAnswerViewModel(SurveyAnswerResponse answerResponse, int votes, bool isChecked) {
            AnswerResponse = answerResponse;
            Votes = votes;
            IsChecked = isChecked;
        }
    }
}
