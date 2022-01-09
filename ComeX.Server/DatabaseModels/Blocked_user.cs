using System;
using System.Collections.Generic;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Blocked_user : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "userId" };

        public Blocked_user(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; set; }

        public object[] Data => new object[] { UserId };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
