using ComeX.UserDatabaseAPI.DAL;
using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public class TokenService : ITokenService
    {
        private ITokenRepository _tokenRepository;
        private IUserRepository _userRepository;

        public TokenService(ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }

        public Task<IEnumerable<Token>> Get()
        {
            return Task.Run<IEnumerable<Token>>(() =>
            {
                return _tokenRepository.Get();
            });
        }
        public Task<Token> Get(string id)
        {
            return Task.Run<Token>(() =>
            {
                return _tokenRepository.Get(id);
            });
        }
        public Task<CommunicationModels.UserDatabaseAPI.TokenMessage> GetTokenInfo(string tokenHash)
        {
            return Task.Run<CommunicationModels.UserDatabaseAPI.TokenMessage>(() =>
            {
                var token = _tokenRepository.GetByHash(tokenHash);
                if (token != null)
                {
                    var user = _userRepository.Get(token.UserId);

                    return new CommunicationModels.UserDatabaseAPI.TokenMessage(user.Username, token.ValidTo);
                }
                else
                    return null;
            });
        }
        public Task<Token> Create(Token token)
        {
            return Task.Run<Token>(() =>
            {
                _tokenRepository.Insert(token);

                return token;
            });
        }
        public Task Update(string id, Token tokenIn)
        {
            return Task.Run(() =>
            {
                _tokenRepository.Update(id, tokenIn);
            });
        }
        public Task Remove(Token tokenIn)
        {
            return Task.Run(() =>
            {
                _tokenRepository.Delete(tokenIn);
            });
        }
        public Task Remove(string Id)
        {
            return Task.Run(() =>
            {
                _tokenRepository.Delete(Id);
            });
        }
    }
}
