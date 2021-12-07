using ComeX.Lib.Common.UserDatabaseAPI;

namespace ComeX.Lib.Auth
{
    internal interface IUserApiManager
    {
        TokenDataModel GetToken(string tokenHash);
    }
}