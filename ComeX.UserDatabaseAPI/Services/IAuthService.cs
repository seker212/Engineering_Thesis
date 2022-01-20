using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public interface IAuthService
    {
        Task<ComeX.Lib.Common.UserDatabaseAPI.TokenDataModel> GetTokenInfo(string tokenHash);
    }
}