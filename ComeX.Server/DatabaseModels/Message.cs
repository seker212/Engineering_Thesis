using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;

namespace ComeX.Server.DatabaseModels
{
    public class Message : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "authorId", "roomId", "sendTime", "parentId", "content" };

        public Message(Guid id, Guid authorId, Guid roomId, DateTime sendTime, Nullable<Guid> parentId, string content)
        {
            Id = id;
            AuthorId = authorId;
            RoomId = roomId;
            SendTime = sendTime;
            ParentId = parentId;
            Content = content;
        }

        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime SendTime { get; set; }
        public Nullable<Guid> ParentId { get; set; }
        public string Content { get; set; }

        public object[] Data => new object[] { Id, AuthorId, RoomId, SendTime, ParentId, Content };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
