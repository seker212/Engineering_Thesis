using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.UserDatabaseAPI
{
    public class ServerDataModel
    {
        public ServerDataModel(string name, string url)
        {
            Name = name;
            Url = url;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Url")]
        public string Url { get; set; }
    }
}
