using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public interface ITokenService
    {
        Task<Token> Create(Token token);
        Task<IEnumerable<Token>> Get();
        Task<Token> Get(string id);
        Task<CommunicationModels.UserDatabaseAPI.TokenMessage> GetTokenInfo(string tokenHash);
        Task Remove(string Id);
        Task Remove(Token tokenIn);
        Task Update(string id, Token tokenIn);
    }
}