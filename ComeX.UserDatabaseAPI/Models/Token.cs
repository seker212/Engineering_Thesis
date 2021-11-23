using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class Token : IDatabaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TokenValue { get; set; }
        public string TokenHash { get; set; }
        public string UserId { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TokenValue is null)
                yield return new ValidationResult("Token value is empty");
            if (TokenHash is null)
                yield return new ValidationResult("TokenHash is empty");
            if (UserId is null)
                yield return new ValidationResult("User ID is empty");
            if (ValidFrom is null)
                yield return new ValidationResult("Start date is empty");
            if (ValidTo is null)
                yield return new ValidationResult("End date is empty");
        }
    }
}
