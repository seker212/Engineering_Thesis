using ComeX.UserDatabaseAPI.DAL;
using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public class AuthService : IAuthService
    {
        private ITokenRepository _tokenRepository;
        private IUserRepository _userRepository;

        public AuthService(ITokenRepository tokenRepository, IUserRepository userRepository)
        {
            _tokenRepository = tokenRepository;
            _userRepository = userRepository;
        }
        public Task<ComeX.Lib.Common.UserDatabaseAPI.TokenDataModel> GetTokenInfo(string tokenHash)
        {
            return Task.Run<ComeX.Lib.Common.UserDatabaseAPI.TokenDataModel>(() =>
            {
                var token = _tokenRepository.GetByHash(tokenHash);
                if (token is not null)
                {
                    var user = _userRepository.Get(token.UserId);

                    if (user is not null)
                        return new ComeX.Lib.Common.UserDatabaseAPI.TokenDataModel(user.UserId, user.Username, token.ValidTo);
                }
                return null;
            });
        }
    }
}
