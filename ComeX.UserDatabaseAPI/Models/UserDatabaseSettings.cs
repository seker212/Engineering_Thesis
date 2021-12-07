using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Models
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string TokensCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
