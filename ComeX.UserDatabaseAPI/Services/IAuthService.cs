using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public interface IAuthService
    {
        Task<ComeX.Lib.Common.UserDatabaseAPI.TokenDataModel> GetTokenInfo(string tokenHash);
    }
}