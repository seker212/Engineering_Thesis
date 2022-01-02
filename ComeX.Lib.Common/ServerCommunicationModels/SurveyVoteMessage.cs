using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    class SurveyVoteMessage {
        public SurveyVoteMessage(string token, List<Guid> answerId) {
            Token = token;
            AnswerId = answerId;
        }

        public SurveyVoteMessage() { }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("AnswerId")]
        public List<Guid> AnswerId { get; set; }
    }
}
