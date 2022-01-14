using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.UserDatabaseAPI
{
    public class Token
    {
        public Token(string id, string tokenValue, string tokenHash, string userId, string validFrom, string validTo)
        {
            Id = id;
            TokenValue = tokenValue;
            TokenHash = tokenHash;
            UserId = userId;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("TokenValue")]
        public string TokenValue { get; set; }
        [JsonProperty("TokenHash")]
        public string TokenHash { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("ValidFrom")]
        public string ValidFrom { get; set; }
        [JsonProperty("ValidTo")]
        public string ValidTo { get; set; }
    }
}
