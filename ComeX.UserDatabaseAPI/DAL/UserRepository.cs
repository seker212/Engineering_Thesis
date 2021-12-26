using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUserDatabaseSettings settings) : base(settings)
        {
            _collection = _database.GetCollection<User>(settings.UsersCollectionName);
        }
        public User GetByUsername(string username) => _collection.Find<User>(user => user.Username == username).FirstOrDefault();
        public User Get(string id) => _collection.Find<User>(user => user.Id == id).FirstOrDefault();
        public void Insert(User user) => _collection.InsertOne(user);
        public void Update(string id, User userIn) => _collection.ReplaceOne(user => user.Id == id, userIn);
        public void Delete(User userIn) => _collection.DeleteOne(user => user.Id == userIn.Id);
        public void Delete(string id) => _collection.DeleteOne(user => user.Id == id);
    }
}
