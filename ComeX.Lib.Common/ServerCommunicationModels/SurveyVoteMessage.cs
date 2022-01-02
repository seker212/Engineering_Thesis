using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class SurveyVoteMessage {
        public SurveyVoteMessage(string token, Guid surveyId, List<Guid> answerId) {
            Token = token;
            SurveyId = surveyId;
            AnswerId = answerId;
        }

        public SurveyVoteMessage() { }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("SurveyId")]
        public Guid SurveyId { get; set; }
        [JsonProperty("AnswerId")]
        public List<Guid> AnswerId { get; set; }
    }
}
