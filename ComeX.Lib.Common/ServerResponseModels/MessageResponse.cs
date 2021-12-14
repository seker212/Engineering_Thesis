using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class MessageResponse {
        public MessageResponse(string username, Guid roomId, Guid parentId, string content, bool hasFile, Dictionary<string, int> emojiList) {
            Username = username;
            RoomId = roomId;
            ParentId = parentId;
            Content = content;
            HasFile = hasFile;
            EmojiList = emojiList;
        }

        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("ParentId")]
        public Guid ParentId { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("HasFile")]
        public bool HasFile { get; set; }
        [JsonProperty("EmojiList")]
        public Dictionary<string, int> EmojiList { get; set; }
    }
}
