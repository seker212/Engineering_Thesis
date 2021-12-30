using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface IUserToServerRepository
    {
        void Delete(string id);
        void Delete(UserToServer entityIn);
        UserToServer Get(string id);
        IEnumerable<UserToServer> GetListByUserId(string id);
        IEnumerable<UserToServer> GetListByServerId(string id);
        void Insert(UserToServer entity);
        void Update(string id, UserToServer entityIn);
    }
}