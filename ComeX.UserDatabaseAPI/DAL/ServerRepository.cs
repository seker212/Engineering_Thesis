using ComeX.UserDatabaseAPI.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.DAL
{
    public class ServerRepository : Repository<Server>, IServerRepository
    {
        public ServerRepository(IUserDatabaseSettings settings) : base(settings)
        {
            _collection = _database.GetCollection<Server>(settings.ServersCollectionName);
        }
        public Server Get(string id) => _collection.Find<Server>(server => server.Id == id).FirstOrDefault();
        public Server GetByName(string name) => _collection.Find<Server>(server => server.Name == name).FirstOrDefault();
        public Server GetByUrl(string url) => _collection.Find<Server>(server => server.Url == url).FirstOrDefault();
        public void Insert(Server server) => _collection.InsertOne(server);
        public void Update(string id, Server serverIn) => _collection.ReplaceOne(server => server.Id == id, serverIn);
        public void Delete(Server serverIn) => _collection.DeleteOne(server => server.Id == serverIn.Id);
        public void Delete(string id) => _collection.DeleteOne(server => server.Id == id);
    }
}
