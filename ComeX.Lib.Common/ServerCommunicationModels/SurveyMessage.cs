using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {

    public class SurveyMessage {
        public SurveyMessage(string token, Guid roomId, string question, List<string> answerList) {
            Token = token;
            RoomId = roomId;
            Question = question;
            AnswerList = answerList;
        }

        public SurveyMessage() { }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Question")]
        public string Question { get; set; }
        [JsonProperty("AnswerList")]
        public List<string> AnswerList { get; set; }
    }
}
