using ComeX.UserDatabaseAPI.DAL;
using ComeX.UserDatabaseAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        public UserService(IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public Task<User> CreateUser(string username, string password)
        {
            return Task.Run<User>(() =>
            {
                UserDatabaseAPI.Helpers.HashingHelper hashingHelper = new UserDatabaseAPI.Helpers.HashingHelper;
                var hashedPassword = hashingHelper.GenerateHash(password);
                var user = new User(username, hashedPassword, hashingHelper.Salt);

                _userRepository.Insert(user);

                return user;
            });
        }

        public Task<IEnumerable<User>> Get()
        {
            return Task.Run<IEnumerable<User>>(() =>
            {
                return _userRepository.Get();
            });
        }
        public Task<User> Get(string id)
        {
            return Task.Run<User>(() =>
            {
                return _userRepository.Get(id);
            });
        }
        public Task<User> Create(User user)
        {
            return Task.Run<User>(() =>
            {
                _userRepository.Insert(user);
                return user;
            });
        }
        public Task Update(string id, User userIn)
        {
            return Task.Run(() =>
            {
                _userRepository.Update(id, userIn);
            });
        }
        public Task Remove(User userIn)
        {
            return Task.Run(() =>
            {
                _userRepository.Delete(userIn);
            });
        }
        public Task Remove(string id)
        {
            return Task.Run(() =>
            {
                _userRepository.Delete(id);
            });
        }
    }
}
