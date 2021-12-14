using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class SurveyAnswerResponse {
        public SurveyAnswerResponse(Guid answerId, string content) {
            AnswerId = answerId;
            Content = content;
        }

        public SurveyAnswerResponse() { }

        [JsonProperty("AnswerId")]
        public Guid AnswerId { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
    }
}
