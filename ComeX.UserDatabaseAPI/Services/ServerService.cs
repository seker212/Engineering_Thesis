using ComeX.UserDatabaseAPI.DAL;
using ComeX.UserDatabaseAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Services
{
    public class ServerService : IServerService
    {
        private IServerRepository _serverRepository;
        private IUserToServerRepository _userToServerRepository;
        private IUserRepository _userRepository;

        public ServerService(IServerRepository serverRepository, IUserToServerRepository userToServerRepository, IUserRepository userRepository)
        {
            _serverRepository = serverRepository;
            _userToServerRepository = userToServerRepository;
            _userRepository = userRepository;
        }

        public Task<(string Reason, Server AddedServer)> CreateServer(string name, string url)
        {
            return Task.Run<(string, Server)>(() =>
            {
                if (!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(url))
                {
                    var checkServer = _serverRepository.GetByName(name);

                    if (checkServer is not null)
                        return ("A server with this name already exists", null);
                    else
                    {
                        var server = new Server(name, url);
                        _serverRepository.Insert(server);

                        return (string.Empty, _serverRepository.GetByName(server.Name));
                    }
                }
                else
                    return ("Name or ulr cannot be empty", null);
            });
        }

        public Task<bool> AddUserToServer(string username, string url)
        {
            return Task.Run<bool>(() =>
            {
                if (!String.IsNullOrEmpty(username) || !String.IsNullOrEmpty(url))
                {
                    var server = _serverRepository.GetByUrl(url);
                    var user = _userRepository.GetByUsername(username);

                    if ((server is not null) || (user is not null))
                    {
                        var userToServers = _userToServerRepository.GetListByUserId(user.Id);
                        foreach (var userToServer in userToServers)
                        {
                            if (userToServer.ServerId == server.Id)
                                return false;
                        }
                        _userToServerRepository.Insert(new UserToServer(user.Id, server.Id));
                        return true;
                    }
                }
                return false;
            });
        }

        public Task<bool> DeleteServer(string url)
        {
            return Task.Run<bool>(() =>
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var server = _serverRepository.GetByUrl(url);
                    var usersToServer = _userToServerRepository.GetListByServerId(server.Id);

                    if (server is not null)
                    {
                        _serverRepository.Delete(server);
                        foreach (var userToServer in usersToServer)
                        {
                            _userToServerRepository.Delete(userToServer);
                        }
                        return true;
                    }
                }
                return false;
            });
        }

        public Task<IEnumerable<Lib.Common.UserDatabaseAPI.ServerDataModel>> GetServers(string username)
        {
            return Task.Run(() =>
            {
                var result = new List<Lib.Common.UserDatabaseAPI.ServerDataModel>();
                if (!string.IsNullOrEmpty(username))
                {
                    var user = _userRepository.GetByUsername(username);
                    var userToServers = _userToServerRepository.GetListByUserId(user.Id);

                    foreach (var userToServer in userToServers)
                    {
                        var server = _serverRepository.Get(userToServer.ServerId);
                        result.Add(new Lib.Common.UserDatabaseAPI.ServerDataModel(server.Name, server.Url));
                    }
                    return result.AsEnumerable();
                }
                return null;
            });
        }
    }
}
