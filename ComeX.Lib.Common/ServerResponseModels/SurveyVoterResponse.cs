using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    class SurveyVoterResponse {
        public SurveyVoterResponse(Guid surveyId, Guid userId, bool voted) {
            SurveyId = surveyId;
            UserId = userId;
            Voted = voted;
        }

        public SurveyVoterResponse() { }

        [JsonProperty("SurveyId")]
        public Guid SurveyId { get; set; }
        [JsonProperty("UserId")]
        public Guid UserId { get; set; }
        [JsonProperty("Voted")]
        public bool Voted { get; set; }
    }
}
