using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class ReactionResponse {
        public ReactionResponse(Guid messageId, string emoji) {
            MessageId = messageId;
            Emoji = emoji;
        }

        [JsonProperty("MessageId")]
        public Guid MessageId { get; set; }
        [JsonProperty("Emoji")]
        public string Emoji { get; set; }
    }
}
