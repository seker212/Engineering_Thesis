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

        public User GetUser(Guid id) => Query().Where("id", id).First<User>();
        public User InsertUser(User usr) => Query().Insert(GenerateDataDictionary(usr, 0)) == 1 ? usr : throw new Exception();
    }
}
