using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class SearchMessageRequest {
        public SearchMessageRequest(string token, Guid roomId, string search) {
            Token = token;
            RoomId = roomId;
            Search = search;
        }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Search")]
        public string Search { get; set; }
    }
}
