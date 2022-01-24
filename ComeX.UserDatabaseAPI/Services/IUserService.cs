using ComeX.UserDatabaseAPI.Models;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public interface IUserService
    {
        Task<(string Reason, User AddedUser)> CreateUser(string username, string password);
        Task<Lib.Common.UserDatabaseAPI.LoginDataModel> Login(string username, string password);
        Task<User> UpdateUser(string username, string password, string newPassword);
        Task<bool> DeleteUser(string username, string password);
    }
}