using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
	
    public class RoomRequest {
        public RoomRequest(string name, Guid serverId, bool isArchived) {
            Name = name;
        }

        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
