using ComeX.Server.DatabaseModels;
using System;

namespace ComeX.Server.DAL
{
    public interface IUserRepository : IObjectRepository<User>
    {
        User GetUser(Guid id);
        User InsertUser(User usr);
    }
}