using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class SurveyAnswerResponse {
        public SurveyAnswerResponse(Guid answerId, string content, int votes) {
            AnswerId = answerId;
            Content = content;
            Votes = votes;
        }

        public SurveyAnswerResponse() { }

        [JsonProperty("AnswerId")]
        public Guid AnswerId { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("Votes")]
        public int Votes { get; set; }
    }
}
