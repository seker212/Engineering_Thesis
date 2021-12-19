using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class VoteRepository : ObjectRepository<Vote>, IVoteRepository
    {
        public VoteRepository(string connectionString) : base(connectionString, "votes", Vote.ColumnNames)
        {
        }

        public IEnumerable<Vote> GetVotes(Guid answerId) => Query().Where("answerId", answerId).Get<Vote>();
    }
}
