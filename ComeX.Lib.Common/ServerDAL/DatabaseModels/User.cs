using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerDAL.DatabaseModels
{
    public class User : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "username", "is_temp" };

        public User(Guid id, string username, bool is_temp)
        {
            Id = id;
            Username = username;
            IsTemp = is_temp;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public bool IsTemp { get; set; }

        public object[] Data => new object[] { Id, Username, IsTemp };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
