using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class SurveyResponse {
        public SurveyResponse(Guid id, string username, DateTime sendTime, Guid roomId, string question, List<SurveyAnswerResponse> answerList) {
            Id = id;
            Username = username;
            SendTime = sendTime;
            RoomId = roomId;
            Question = question;
            AnswerList = answerList;
        }

        public SurveyResponse() { }

        [JsonProperty("Id")]
        public Guid Id { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("SendTime")]
        public DateTime SendTime { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Question")]
        public string Question { get; set; }
        [JsonProperty("AnswerList")]
        public List<SurveyAnswerResponse> AnswerList { get; set; }
    }
}
