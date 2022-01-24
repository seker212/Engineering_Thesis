using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels
{
    public class SurveyVoteResponse
    {
        public SurveyVoteResponse(SurveyResponse survey, bool voted)
        {
            Survey = survey;
            Voted = voted;
        }

        public SurveyVoteResponse() { }

        [JsonProperty("Survey")]
        public SurveyResponse Survey { get; set; }
        [JsonProperty("Voted")]
        public bool Voted { get; set; }
    }
}
