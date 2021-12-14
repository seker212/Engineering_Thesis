using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Survey : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "authorId", "roomId", "sendTime", "question", "isMultipleChoice" };

        public Survey(Guid id, Guid authorId, Guid roomId, string sendTime, string question, bool isMultipleChoice)
        {
            Id = id;
            AuthorId = authorId;
            RoomId = roomId;
            SendTime = sendTime;
            Question = question;
            IsMultipleChoice = isMultipleChoice;
        }

        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid RoomId { get; set; }
        public string SendTime { get; set; }
        public string Question { get; set; }
        public bool IsMultipleChoice { get; set; }

        public object[] Data => new object[] { Id, AuthorId, RoomId, SendTime, Question, IsMultipleChoice };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
