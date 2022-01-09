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

        public Message GetMessage(Guid id) => Query().Where("id", id).First<Message>();
        public IEnumerable<Message> GetRoomMessages(Guid roomId, string sendTime) => Query().Where("roomId", roomId).WhereTime("sendTime", "=<", sendTime).Limit(200).Get<Message>();
        public IEnumerable<Message> FindMessages(Guid roomId, string search) => Query().Where("roomId", roomId).WhereContains("content", search).Limit(20).Get<Message>();
        public Message InsertMessage(Message msg) => Query().Insert(GenerateDataDictionary(msg, 0)) == 1 ? msg : throw new Exception();
    }
}
