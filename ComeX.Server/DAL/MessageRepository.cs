using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
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

        public Message GetMessage(Guid id) => Get(id);
        public IEnumerable<Message> GetRoomMessages(Guid roomId, DateTime sendTime) => Query().Where("roomId", roomId).Where("sendTime", "<", sendTime).OrderByDesc("sendTime").Limit(50).Get<Message>();
        public IEnumerable<Message> FindMessages(Guid roomId, string search) => Query().Where("roomId", roomId).WhereContains("content", search).Limit(20).Get<Message>();
        public Message InsertMessage(Message msg) => Insert(msg);
    }
}
