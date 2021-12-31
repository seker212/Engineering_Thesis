using ComeX.UserDatabaseAPI.Models;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface IServerRepository
    {
        void Delete(Server serverIn);
        void Delete(string id);
        Server Get(string id);
        Server GetByName(string name);
        Server GetByUrl(string url);
        void Insert(Server server);
        void Update(string id, Server serverIn);
    }
}