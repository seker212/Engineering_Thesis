using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class LoadMessageRequest {
        public LoadMessageRequest(string token, Guid id) {
            Token = token;
            Id = id;
        }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("Id")]
        public Guid Id { get; set; }
    }
}
