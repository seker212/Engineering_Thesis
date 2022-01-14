using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public interface IServerService
    {
        Task<bool> AddUserToServer(string username, string url);
        Task<(string Reason, Server AddedServer)> CreateServer(string name, string url);
        Task<bool> DeleteServer(string url);
        Task<IEnumerable<Lib.Common.UserDatabaseAPI.ServerDataModel>> GetServers(string username);
    }
}