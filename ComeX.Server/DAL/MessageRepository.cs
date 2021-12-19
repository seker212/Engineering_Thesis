using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class MessageRepository : ObjectRepository<Message>, IMessageRepository
    {
        public MessageRepository(string connectionString) : base(connectionString, "messages", Message.ColumnNames)
        {
        }

        public Message GetMessage(Guid id) => Query().Where("id", id).First<Message>();
        public IEnumerable<Message> GetRoomMessages(Guid roomId, string sendTime) => Query().Where("roomId", roomId).WhereDate("sendTime", sendTime).Get<Message>();
    }
}
