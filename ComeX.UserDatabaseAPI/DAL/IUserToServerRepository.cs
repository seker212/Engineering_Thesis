using ComeX.UserDatabaseAPI.Models;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface IUserToServerRepository
    {
        void Delete(string id);
        void Delete(UserToServer entityIn);
        UserToServer Get(string id);
        void Insert(UserToServer entity);
        void Update(string id, UserToServer entityIn);
    }
}