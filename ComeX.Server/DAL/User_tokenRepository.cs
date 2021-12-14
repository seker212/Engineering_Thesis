using System;
using ComeX.Server.DatabaseModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlKata.Execution;

namespace ComeX.Server.DAL
{
    public class User_tokenRepository : ObjectRepository<User_token>, IUser_tokenRepository
    {
        public User_tokenRepository(string connectionString) : base(connectionString, "user_tokens", User_token.ColumnNames)
        {
        }

        public User_token GetToken(string tokenHash) => Query().Where("tokenHash", tokenHash).First<User_token>();
    }
}
