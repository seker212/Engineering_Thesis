using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComeX.UserDatabaseAPI.Models
{
    public class Token : IDatabaseModel
    {
        public Token(string? id, string tokenValue, string tokenHash, string userId, string validFrom, string validTo)
        {
            Id = id;
            TokenValue = tokenValue;
            TokenHash = tokenHash;
            UserId = userId;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string TokenValue { get; set; }
        public string TokenHash { get; set; }
        public string UserId { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}
