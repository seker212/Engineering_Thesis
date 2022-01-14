using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerDAL.DatabaseModels
{
    public class Allowed_user : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "userId" };

        public Allowed_user(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public object[] Data => new object[] { UserId };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
