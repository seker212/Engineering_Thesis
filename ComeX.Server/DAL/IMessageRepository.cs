using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IMessageRepository : IObjectRepository<Message>
    {
        Message GetMessage(Guid id);
        IEnumerable<Message> GetRoomMessages(Guid roomId, DateTime sendTime);
        public IEnumerable<Message> FindMessages(Guid roomId, string search);
        public Message InsertMessage(Message msg);
    }
}