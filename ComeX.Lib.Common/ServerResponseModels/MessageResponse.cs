using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class MessageResponse {
        public MessageResponse() { }
        public MessageResponse(Guid id, string username, DateTime sendTime, Guid roomId, Nullable<Guid> parentId, string content, Dictionary<string, int> emojiList) {
            Id = id;
            Username = username;
            SendTime = sendTime;
            RoomId = roomId;
            ParentId = parentId;
            Content = content;
            EmojiList = emojiList;
        }

        [JsonProperty("Id")]
        public Guid Id { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("SendTime")]
        public DateTime SendTime { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("ParentId")]
        public Nullable<Guid> ParentId { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("EmojiList")]
        public Dictionary<string, int> EmojiList { get; set; }
    }
}
