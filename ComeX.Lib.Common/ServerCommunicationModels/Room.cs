using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class Room {
        public Room(string name, Guid serverId, bool isArchived) {
            Name = name;
            ServerId = serverId;
            IsArchived = isArchived;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ServerId")]
        public Guid ServerId { get; set; }
        [JsonProperty("IsArchived")]
        public bool IsArchived { get; set; }
    }
}
