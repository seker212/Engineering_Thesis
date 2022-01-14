using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;

namespace ComeX.Lib.Common.ServerDAL
{
    public interface IUserRepository : IObjectRepository<User>
    {
        User GetUser(Guid id);
        User InsertUser(User usr);
    }
}