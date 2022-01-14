using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class LoadAllResponse {
        public LoadAllResponse(Guid roomId, List<MessageResponse> messageList, List<SurveyResponse> surveyList) {
            RoomId = roomId;
            MessageList = messageList;
            SurveyList = surveyList;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("MessageList")]
        public List<MessageResponse> MessageList { get; set; }
        [JsonProperty("SurveyList")]
        public List<SurveyResponse> SurveyList { get; set; }
    }
}
