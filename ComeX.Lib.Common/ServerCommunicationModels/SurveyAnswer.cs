using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class SurveyAnswer {
        public SurveyAnswer(string content, Guid surveyId) {
            Content = content;
            SurveyId = surveyId;
        }

        public SurveyAnswer() { }

        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("SurveyId")]
        public Guid SurveyId { get; set; }
    }
}
