using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class ReactionMessage {
        public ReactionMessage(string token, Guid messageId, string emoji) {
            Token = token;
            MessageId = messageId;
            Emoji = emoji;
        }
        public ReactionMessage() { }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("MessageId")]
        public Guid MessageId { get; set; }
        [JsonProperty("Emoji")]
        public String Emoji { get; set; }
    }
}
