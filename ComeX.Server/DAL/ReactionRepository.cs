using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class ReactionRepository : ObjectRepository<Reaction>, IReactionRepository
    {
        public ReactionRepository(string connectionString) : base(connectionString, "reactions", Reaction.ColumnNames)
        {
        }

        public IEnumerable<Reaction> GetReactions(Guid messageId) => Query().Where("messageId", messageId).Get<Reaction>();
    }
}
