using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Lib.Common.ServerDAL
{
    public class UserRepository : ObjectRepository<User>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString, "users", User.ColumnNames)
        {
        }

        public User GetUser(Guid id) => Get(id);
        public User InsertUser(User usr) => Insert(usr);
        public bool UpdateToNormalUser(Guid newId, string username) => Query().Where("username", username).WhereTrue("is_temp").Update(new Dictionary<string, object>() { { "id", newId }, { "is_temp", false } }) == 1;
    }
}
