using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels
{
    public class LoginMessage
    {
        public LoginMessage(string token)
        {
            Token = token;
        }

        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
