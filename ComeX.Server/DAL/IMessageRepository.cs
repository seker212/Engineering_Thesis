﻿using ComeX.Server.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IMessageRepository : IObjectRepository<Message>
    {
        Message GetMessage(Guid id);
        IEnumerable<Message> GetRoomMessages(Guid roomId, string sendTime);
        public Message InsertMessage(Message msg);
    }
}