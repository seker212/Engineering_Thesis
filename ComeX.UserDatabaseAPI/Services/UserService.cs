using ComeX.UserDatabaseAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IUserDatabaseSettings settings)
        {
            var dbClient = new MongoClient(settings.ConnectionString);
            var database = dbClient.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public Task<List<User>> Get()
        {
            return Task.Run<List<User>>(() =>
            {
                return _users.Find(user => true).ToList();
            });
        }
        public Task<User> Get(string id)
        {
            return Task.Run<User>(() =>
            {
                return _users.Find<User>(user => user.Id == id).FirstOrDefault();
            });
        }
        public Task<User> Create(User user)
        {
            return Task.Run<User>(() =>
            {
                _users.InsertOne(user);
                return user;
            });
        }
        public Task Update(string id, User userIn)
        {
            return Task.Run(() =>
            {
                _users.ReplaceOne(user => user.Id == id, userIn);
            });
        }
        public Task Remove(User userIn)
        {
            return Task.Run(() =>
            {
                _users.DeleteOne(user => user.Id == userIn.Id);
            });
        }
        public Task Remove(string id)
        {
            return Task.Run(() =>
            {
                _users.DeleteOne(user => user.Id == id);
            });
        }
    }
}
