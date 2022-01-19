using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class LoadAllResponse {
        public LoadAllResponse(Guid roomId, List<MessageResponse> messageList, LoadSurveyVoteResponse surveyVoted) {
            RoomId = roomId;
            MessageList = messageList;
            SurveyVoted = surveyVoted;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("MessageList")]
        public List<MessageResponse> MessageList { get; set; }
        [JsonProperty("SurveyVote")]
        public LoadSurveyVoteResponse SurveyVoted { get; set; }
    }
}
