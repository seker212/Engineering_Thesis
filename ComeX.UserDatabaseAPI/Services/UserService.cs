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
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
