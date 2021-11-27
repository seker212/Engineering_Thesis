using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.CommunicationModels.UserDatabaseAPI
{
    public class TokenMessage
    {
        public TokenMessage(string username, string validTo)
        {
            Username = username;
            ValidTo = validTo;
        }

        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("ValidTo")]
        public string ValidTo { get; set; }
    }
}
