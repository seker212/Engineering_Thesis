using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.UserDatabaseAPI
{
    public class User
    {
        public User(string id, string userId, string username, string passwordHash, string salt)
        {
            Id = id;
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }

        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("UserId")]
        public string UserId { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("PasswordHash")]
        public string PasswordHash { get; set; }
        [JsonProperty("Salt")]
        public string Salt { get; set; }
    }
}
