using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class Blocked_userRepository : ObjectRepository<Blocked_user>, IBlocked_userRepository
    {
        public Blocked_userRepository(string connectionString) : base(connectionString, "blocked_users", Blocked_user.ColumnNames)
        {
        }

        public Blocked_user GetBlocked_user(Guid userId) => Query().Where("userId", userId).First<Blocked_user>();
    }
}
