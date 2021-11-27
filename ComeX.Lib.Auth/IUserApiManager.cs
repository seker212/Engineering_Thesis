using ComeX.Lib.Common.UserDatabaseAPI;

namespace ComeX.Lib.Auth
{
    internal interface IUserApiManager
    {
        TokenMessage GetToken(string tokenHash);
    }
}