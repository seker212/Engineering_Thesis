using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    class RoomResponse {
        public RoomResponse(string name, bool isArchived, List<MessageResponse> messageList) {
            Name = name;
            IsArchived = isArchived;
            MessageList = messageList;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("IsArchived")]
        public bool IsArchived { get; set; }
        [JsonProperty("MessageList")]
        public List<MessageResponse> MessageList { get; set; }
    }
}
