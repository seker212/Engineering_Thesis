using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Blocked_users : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "userId" };

        public Blocked_users(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public object[] Data => new object[] { UserId };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
