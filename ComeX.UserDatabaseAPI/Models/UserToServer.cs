using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class UserToServer : IDatabaseModel
    {
        public UserToServer(string id, string userId, string serverId)
        {
            Id = id;
            UserId = userId;
            ServerId = serverId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ServerId { get; set; }
    }
}
