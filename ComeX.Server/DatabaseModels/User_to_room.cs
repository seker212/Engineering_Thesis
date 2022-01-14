using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;

namespace ComeX.Server.DatabaseModels
{
    public class User_to_room : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "userId", "roomId", };

        public User_to_room(Guid userId, Guid roomId)
        {
            UserId = userId;
            RoomId = roomId;
        }

        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }

        public object[] Data => new object[] { UserId, RoomId };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
