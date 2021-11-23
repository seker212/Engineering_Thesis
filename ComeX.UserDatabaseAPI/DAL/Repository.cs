using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.DAL
{
    public abstract class Repository<T> : IRepository<T> where T : IDatabaseModel
    {
        protected MongoClient _client;
        protected IMongoDatabase _database;
        protected IMongoCollection<T> _collection;
        protected Repository(IUserDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
        }
    }
}
