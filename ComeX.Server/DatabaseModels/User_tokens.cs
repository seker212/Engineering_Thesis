using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class User_tokens : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "tokenHash", "userId", "validFrom", "validTo" };

        public User_tokens(string tokenHash, Guid userId, string validFrom, string validTo)
        {
            TokenHash = tokenHash;
            UserId = userId;
            ValidFrom = validFrom;
            ValidTo = validTo;
        }

        public string TokenHash { get; set;}
        public Guid UserId { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }

        public object[] Data => new object[] { TokenHash, UserId, ValidFrom, ValidTo };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
