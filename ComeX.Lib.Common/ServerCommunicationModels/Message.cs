﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComeX.Lib.Common.ServerCommunicationModels {
    public class Message {
        public Message(string token, Guid roomId, Guid parentId, string content, bool hasFile) {
            Token = token;
            RoomId = roomId;
            ParentId = parentId;
            Content = content;
            HasFile = hasFile;
        }

        [JsonProperty("Token")]
        public string Token { get; set; }
        [JsonProperty("RoomId")]
        public Guid RoomId { get; set; }
        [JsonProperty("ParentId")]
        public Guid ParentId { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
        [JsonProperty("HasFile")]
        public bool HasFile { get; set; }
    }
}