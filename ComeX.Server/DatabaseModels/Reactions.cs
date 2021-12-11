using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DatabaseModels
{
    public class Reactions : IDbModel
    {
        private static readonly string[] _staticColumnNames = new string[] { "id", "userId", "messageId", "emoji" };

        public Reactions(Guid id, Guid userId, Guid messageId, string emoji)
        {
            Id = id;
            UserId = userId;
            MessageId = messageId;
            Emoji = emoji;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MessageId { get; set; }
        public string Emoji { get; set; }

        public object[] Data => new object[] { Id, UserId, MessageId, Emoji };
        public static string[] ColumnNames => _staticColumnNames;
    }
}
