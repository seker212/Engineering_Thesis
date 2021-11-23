using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public interface IUserService
    {
        Task<User> Create(User user);
        Task<IEnumerable<User>> Get();
        Task<User> Get(string id);
        Task Remove(string id);
        Task Remove(User userIn);
        Task Update(string id, User userIn);
    }
}