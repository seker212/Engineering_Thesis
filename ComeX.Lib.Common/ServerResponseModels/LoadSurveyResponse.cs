using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class LoadSurveyResponse {
        public LoadSurveyResponse(Guid roomId, List<SurveyResponse> surveyList) {
            RoomId = roomId;
            SurveyList = surveyList;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("MessageList")]
        public List<SurveyResponse> SurveyList { get; set; }
    }
}
}
