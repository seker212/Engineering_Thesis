﻿using ComeX.Server.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IReactionRepository : IObjectRepository<Reaction>
    {
        IEnumerable<Reaction> GetReactions(Guid messageId);
        public Reaction GetReaction(Guid userId, Guid messageId, string emoji);
        public Reaction InsertReaction(Reaction rec);
    }
}