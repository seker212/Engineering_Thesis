using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class User : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "username" };

        public User(Guid id, string username)
        {
            Id = id;
            Username = username;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }

        public object[] Data => new object[] { Id, Username };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
