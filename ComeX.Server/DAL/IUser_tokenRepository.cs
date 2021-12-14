using ComeX.Server.DatabaseModels;

namespace ComeX.Server.DAL
{
    public interface IUser_tokenRepository : IObjectRepository<User_token>
    {
        User_token GetToken(string tokenHash);
    }
}