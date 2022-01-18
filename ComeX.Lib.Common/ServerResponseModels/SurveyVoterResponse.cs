using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class SurveyVoterResponse {
        public SurveyVoterResponse(Guid surveyId, Guid userId, Guid roomId, bool voted) {
            SurveyId = surveyId;
            UserId = userId;
            RoomId = roomId;
            Voted = voted;
        }

        public SurveyVoterResponse() { }

        [JsonProperty("SurveyId")]
        public Guid SurveyId { get; set; }
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Voted")]
        public bool Voted { get; set; }
    }
}
