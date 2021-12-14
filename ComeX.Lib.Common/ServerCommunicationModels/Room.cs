using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class Room {
        public Room(string name) {
            Name = name;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
