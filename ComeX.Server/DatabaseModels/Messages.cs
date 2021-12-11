using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Messages : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "authorId", "hasFile", "roomId", "sendTime", "parentId", "content" };

        public Messages(Guid id, Guid authorId, bool hasFile, Guid roomId, string sendTime, Guid parentId, string content)
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
        public Guid ParentId { get; set; }
        public string Content { get; set; }

        public object[] Data => new object[] { Id, AuthorId, HasFile, RoomId, SendTime, ParentId, Content };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
