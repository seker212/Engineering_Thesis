using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.DAL
{
    public class UserToServerRepository : Repository<UserToServer>, IUserToServerRepository
    {
        public UserToServerRepository(IUserDatabaseSettings settings) : base(settings)
        {
            _collection = _database.GetCollection<UserToServer>(settings.UsersToServersCollectionName);
        }
        public UserToServer Get(string id) => _collection.Find<UserToServer>(entity => entity.Id == id).FirstOrDefault();
        public void Insert(UserToServer entity) => _collection.InsertOne(entity);
        public void Update(string id, UserToServer entityIn) => _collection.ReplaceOne(entity => entity.Id == id, entityIn);
        public void Delete(UserToServer entityIn) => _collection.DeleteOne(entity => entity.Id == entityIn.Id);
        public void Delete(string id) => _collection.DeleteOne(entity => entity.Id == id);
    }
}
