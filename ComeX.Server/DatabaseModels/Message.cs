using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Message : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "authorId", "hasFile", "roomId", "sendTime", "parentId", "content" };

        public Message(Guid id, Guid authorId, bool hasFile, Guid roomId, string sendTime, Nullable<Guid> parentId, string content)
        {
            Id = id;
            AuthorId = authorId;
            HasFile = hasFile;
            RoomId = roomId;
            SendTime = sendTime;
            ParentId = parentId;
            Content = content;
        }

        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public bool HasFile { get; set; }
        public Guid RoomId { get; set; }
        public string SendTime { get; set; }
        public Nullable<Guid> ParentId { get; set; }
        public string Content { get; set; }

        public object[] Data => new object[] { Id, AuthorId, HasFile, RoomId, SendTime, ParentId, Content };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
