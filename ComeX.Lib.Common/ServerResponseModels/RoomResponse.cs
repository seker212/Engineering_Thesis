using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class RoomResponse {
        public RoomResponse(Guid roomId, string name, bool isArchived, List<MessageResponse> messageList) {
            RoomId = roomId;
            Name = name;
            IsArchived = isArchived;
            MessageList = messageList;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("IsArchived")]
        public bool IsArchived { get; set; }
        [JsonProperty("MessageList")]
        public List<MessageResponse> MessageList { get; set; }
    }
}
