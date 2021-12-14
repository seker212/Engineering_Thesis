﻿using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class UserRepository : ObjectRepository<User>, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString, "users", User.ColumnNames)
        {
        }

        public User GetUser(Guid id) => Query().Where("id", id).First<User>();
    }
}
