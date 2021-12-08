using ComeX.UserDatabaseAPI.DAL;
using ComeX.UserDatabaseAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        private const int TOKEN_DURATION = 1;
        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public Task<User> CreateUser(string username, string password)
        {
            return Task.Run<User>(() =>
            {
                if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(password))
                {
                    var hashingHelper = new UserDatabaseAPI.Helpers.HashingHelper();
                    var hashedPassword = hashingHelper.GenerateHash(password);
                    var user = new User(Guid.NewGuid().ToString(), username, hashedPassword, hashingHelper.Salt);

                    _userRepository.Insert(user);

                    return _userRepository.Get(user.Id);
                }
                else
                    return null;
            });
        }
        public Task<Lib.Common.UserDatabaseAPI.LoginDataModel> Login(string username, string password)
        {
            return Task.Run<Lib.Common.UserDatabaseAPI.LoginDataModel>(() =>
            {
                if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(password))
                {
                    var user = _userRepository.GetByUsername(username);

                    if (user is not null)
                    {
                        var hashingHelper = new UserDatabaseAPI.Helpers.HashingHelper();
                        hashingHelper.Salt = user.Salt;
                        var hashedPassword = hashingHelper.GenerateHash(password);

                        if (hashedPassword == user.PasswordHash)
                        {
                            var tokenGenerator = new Helpers.TokenGenerator();
                            var token =_tokenRepository.GetByUserId(user.Id);
                            var validFrom = DateTime.UtcNow;
                            var validTo = validFrom.AddDays(TOKEN_DURATION);

                            if (token is null)
                            {
                                var tokenValue = tokenGenerator.GenerateToken();
                                var tokenHash = tokenGenerator.GenerateTokenHash(tokenValue);

                                _tokenRepository.Insert(new Token(null, tokenValue, tokenHash, user.Id, validFrom.ToString(), validTo.ToString()));
                                return new Lib.Common.UserDatabaseAPI.LoginDataModel(tokenValue, user.Username);
                            }
                            else if (new UserDatabaseAPI.Helpers.DateCheckHelper().IsNotExpired(token.ValidTo))
                            {
                                return new Lib.Common.UserDatabaseAPI.LoginDataModel(token.TokenValue, user.Username);
                            }
                            else
                            {
                                var newTokenValue = tokenGenerator.GenerateToken();
                                var newTokenHash = tokenGenerator.GenerateTokenHash(newTokenValue);

                                _tokenRepository.Update(token.Id, new Token(token.Id, newTokenValue, newTokenHash, user.Id, validFrom.ToString(), validTo.ToString()));
                                return new Lib.Common.UserDatabaseAPI.LoginDataModel(newTokenValue, user.Username);
                            }
                        }
                    }
                }
                return null;
            });
        }
        public Task<User> UpdateUser(string username, string password, string newPassword)
        {
            return Task.Run<User>(() =>
            {
                if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(password))
                {
                    var user = _userRepository.GetByUsername(username);
                    if (user is not null)
                    {
                        var hashingHelper = new UserDatabaseAPI.Helpers.HashingHelper();
                        hashingHelper.Salt = user.Salt;
                        var hashedPassword = hashingHelper.GenerateHash(password);

                        if (hashedPassword == user.PasswordHash)
                        {
                            hashingHelper.GenerateSalt();
                            var newHashedPassword = hashingHelper.GenerateHash(newPassword);
                            var newUser = new User(user.UserId, username, newHashedPassword, hashingHelper.Salt);
                            newUser.Id = user.Id;

                            _userRepository.Update(user.Id, newUser);
                            return newUser;
                        }
                    }
                }
                return null;
            });
        }
        public Task<bool> DeleteUser(string username, string password)
        {
            return Task.Run<bool>(() =>
            {
                if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(password))
                {
                    var user = _userRepository.GetByUsername(username);
                    if (user is not null)
                    {
                        var hashingHelper = new UserDatabaseAPI.Helpers.HashingHelper();
                        hashingHelper.Salt = user.Salt;
                        var hashedPassword = hashingHelper.GenerateHash(password);

                        if (hashedPassword == user.PasswordHash)
                        {
                            var token = _tokenRepository.GetByUserId(user.Id);
                            if (token is not null)
                                _tokenRepository.Delete(token);
                        }

                        _userRepository.Delete(user);
                        return true;
                    }
                }
                return false;
            });
        }
    }
}
