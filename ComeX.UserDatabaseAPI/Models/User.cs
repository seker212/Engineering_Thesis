using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
