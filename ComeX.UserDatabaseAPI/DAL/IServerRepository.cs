using ComeX.UserDatabaseAPI.Models;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface IServerRepository
    {
        void Delete(Server serverIn);
        void Delete(string id);
        Server Get(string id);
        void Insert(Server server);
        void Update(string id, Server serverIn);
    }
}