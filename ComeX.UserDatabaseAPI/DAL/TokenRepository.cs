using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.DAL
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(IUserDatabaseSettings settings) : base(settings)
        {
            _collection = _database.GetCollection<Token>(settings.TokensCollectionName);
        }
        public IEnumerable<Token> Get() => _collection.Find(token => true).ToList();
        public Token Get(string id) => _collection.Find<Token>(token => token.Id == id).FirstOrDefault();
        public Token GetByHash(string tokenHash) => _collection.Find<Token>(token => token.TokenHash == tokenHash).FirstOrDefault();
        public void Insert(Token token) => _collection.InsertOne(token);
        public void Update(string id, Token tokenIn) => _collection.ReplaceOne(token => token.Id == id, tokenIn);
        public void Delete(Token tokenIn) => _collection.DeleteOne(token => token.Id == tokenIn.Id);
        public void Delete(string id) => _collection.DeleteOne(token => token.Id == id);
        public void DeleteByHash(string tokenHash) => _collection.DeleteOne(token => token.TokenHash == tokenHash);
    }
}
