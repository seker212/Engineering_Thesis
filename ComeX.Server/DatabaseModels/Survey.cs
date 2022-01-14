using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;

namespace ComeX.Server.DatabaseModels
{
    public class Survey : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "authorId", "roomId", "sendTime", "question" };

        public Survey(Guid id, Guid authorId, Guid roomId, DateTime sendTime, string question)
        {
            Id = id;
            AuthorId = authorId;
            RoomId = roomId;
            SendTime = sendTime;
            Question = question;
        }

        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime SendTime { get; set; }
        public string Question { get; set; }

        public object[] Data => new object[] { Id, AuthorId, RoomId, SendTime, Question };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
