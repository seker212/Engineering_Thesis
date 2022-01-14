using ComeX.Server.DatabaseModels;
using ComeX.Lib.Common.ServerDAL;
using ComeX.Lib.Common.ServerDAL.DatabaseModels;

namespace ComeX.Server.DAL
{
    public interface IUser_tokenRepository : IObjectRepository<User_token>
    {
        User_token GetToken(string tokenHash);
    }
}