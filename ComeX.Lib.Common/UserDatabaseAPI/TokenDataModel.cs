using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.UserDatabaseAPI
{
    public class TokenDataModel
    {
        public TokenDataModel(string userId ,string username, string validTo)
        {
            UserId = userId;
            Username = username;
            ValidTo = validTo;
        }

        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("ValidTo")]
        public string ValidTo { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (!(obj is TokenDataModel))
                return false;
            return (this.UserId == ((TokenDataModel)obj).UserId) && (this.Username == ((TokenDataModel)obj).Username) && (this.ValidTo == ((TokenDataModel)obj).ValidTo);
        }

    }
}
