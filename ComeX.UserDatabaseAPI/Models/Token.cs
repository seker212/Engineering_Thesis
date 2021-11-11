using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class Token
    {
        public ObjectId Id { get; set; }
        public string TokenValue { get; set; }
        public string TokenHash { get; set; }
        public Guid UserId { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}
