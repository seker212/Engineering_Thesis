using ComeX.Server.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IVoteRepository : IObjectRepository<Vote>
    {
        IEnumerable<Vote> GetVotes(Guid answerId);
        public Vote InsertVote(Vote vot);
    }
}