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
    public class VoteRepository : ObjectRepository<Vote>, IVoteRepository
    {
        public VoteRepository(string connectionString) : base(connectionString, "votes", Vote.ColumnNames)
        {
        }

        public IEnumerable<Vote> GetVotes(Guid answerId) => Query().Where("answerId", answerId).Get<Vote>();
        public Vote GetVote(Guid answerId, Guid userId) => Query().Where("answerId", answerId).Where("userId", userId).FirstOrDefault<Vote>();
        public Vote InsertVote(Vote vot) => Query().Insert(GenerateDataDictionary(vot, 0)) == 1 ? vot : throw new Exception();
    }
}
