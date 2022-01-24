using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComeX.UserDatabaseAPI.Models
{
    public class User : IDatabaseModel
    {
        public User(string userId, string username, string passwordHash, string salt)
        {
            UserId = userId;
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
