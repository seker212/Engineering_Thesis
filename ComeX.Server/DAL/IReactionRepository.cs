using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;
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