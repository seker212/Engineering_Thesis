using ComeX.UserDatabaseAPI.Models;
using System.Collections.Generic;

namespace ComeX.UserDatabaseAPI.DAL
{
    public interface ITokenRepository : IRepository<Token>
    {
        void Delete(string id);
        void Delete(Token tokenIn);
        void DeleteByHash(string tokenHash);
        IEnumerable<Token> Get();
        Token Get(string id);
        Token GetByHash(string tokenHash);
        void Insert(Token token);
        void Update(string id, Token tokenIn);
    }
}