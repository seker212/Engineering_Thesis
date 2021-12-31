using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class Server : IDatabaseModel
    {
        public Server(string name, string url)
        {
            Name = name;
            Url = url;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name is null)
                yield return new ValidationResult("Name is empty");
            if (Url is null)
                yield return new ValidationResult("URL is empty");
        }
    }
}
