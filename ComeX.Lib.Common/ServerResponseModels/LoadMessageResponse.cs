using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class LoadMessageResponse {
        public LoadMessageResponse(MessageResponse message) {
            Message = message;
        }

        [JsonProperty("Message")]
        public MessageResponse Message { get; set; }
    }
}
