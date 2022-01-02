using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class LoadSurveyRequest {
        public LoadSurveyRequest(string token, Guid roomId, string date) {
            Token = token;
            RoomId = roomId;
            Date = date;
        }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Date")]
        public string Date { get; set; }
    }
}
