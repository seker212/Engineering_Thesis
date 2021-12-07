using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUsername(string username);
        User Get(string id);
        void Insert(User user);
        void Update(string id, User userIn);
        void Delete(User userIn);
        void Delete(string id);
    }
}