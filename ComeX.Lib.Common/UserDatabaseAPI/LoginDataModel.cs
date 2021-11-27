using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.UserDatabaseAPI
{
    public class LoginDataModel
    {
        public LoginDataModel(string token, string username)
        {
            Token = token;
            Username = username;
        }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("Username")]
        public string Username { get; set; }
    }
}
