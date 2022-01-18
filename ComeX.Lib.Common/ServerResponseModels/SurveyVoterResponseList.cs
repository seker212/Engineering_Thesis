using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels
{
    public class SurveyVoterResponseList
    {
        public SurveyVoterResponseList(List<SurveyVoterResponse> voterList)
        {
            VoterList = voterList;
        }

        public SurveyVoterResponseList() { }

        [JsonProperty("VoterList")]
        public List<SurveyVoterResponse> VoterList { get; set; }
        
    }
}
