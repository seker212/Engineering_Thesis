using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class LoadChatResponse {
        public LoadChatResponse(Guid roomId, List<MessageResponse> messageList) {
            RoomId = roomId;
            MessageList = messageList;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("MessageList")]
        public List<MessageResponse> MessageList { get; set; }
    }
}
