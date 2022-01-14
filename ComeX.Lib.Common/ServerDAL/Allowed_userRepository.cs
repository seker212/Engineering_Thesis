using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerDAL
{
    public class Allowed_userRepository : ObjectRepository<Allowed_user>, IAllowed_userRepository
    {
        public Allowed_userRepository(string connectionString) : base(connectionString, "allowed_users", Allowed_user.ColumnNames)
        {
        }

        public Allowed_user GetAllowed_user(Guid userId) => Query().Where("userId", userId).FirstOrDefault<Allowed_user>();
    }
}
