using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class LoadSurveyVoteResponse {
        public LoadSurveyVoteResponse(Guid roomId, List<SurveyVoteResponse> surveyVoteList) {
            RoomId = roomId;
            SurveyVoteList = surveyVoteList;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("SurveyVoteList")]
        public List<SurveyVoteResponse> SurveyVoteList { get; set; }
    }
}
