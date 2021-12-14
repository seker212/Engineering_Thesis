using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerResponseModels {
    public class RoomsListResponse {
        public RoomsListResponse(List<RoomResponse> roomsList) {
            RoomsList = roomsList;
        }

        [JsonProperty("RoomsList")]
        public List<RoomResponse> RoomsList { get; set; }
    }
}
