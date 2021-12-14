using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Vote : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "userId", "answerId" };

        public Vote(Guid id, Guid userId, Guid answerId)
        {
            Id = id;
            UserId = userId;
            AnswerId = answerId;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AnswerId { get; set; }

        public object[] Data => new object[] { Id, UserId, AnswerId };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
