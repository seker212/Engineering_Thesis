using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class User : IDatabaseModel
    {
        public User(string username, string passwordHash, string salt)
        {
            Username = username;
            PasswordHash = passwordHash;
            Salt = salt;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Username is null)
                yield return new ValidationResult("Username is empty");
            if (PasswordHash is null)
                yield return new ValidationResult("PasswordHash is empty");
        }
    }
}
