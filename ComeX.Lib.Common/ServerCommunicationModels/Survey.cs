using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class Survey {
        public Survey(string token, Guid roomId, string question, bool isMultipleChoice, List<SurveyAnswer> answerList) {
            Token = token;
            RoomId = roomId;
            Question = question;
            IsMultipleChoice = isMultipleChoice;
            AnswerList = answerList;
        }

        public Survey() { }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Question")]
        public string Question { get; set; }
        [JsonProperty("IsMultipleChoice")]
        public bool IsMultipleChoice { get; set; }
        [JsonProperty("AnswerList")]
        public List<SurveyAnswer> AnswerList { get; set; }
    }
}
