using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    class LoginManager
    {
        private UserApiManager _userApiManager;
        private ConnectionCache _connectionCache;

        public LoginManager(UserApiManager userApiManager, ConnectionCache connectionCache)
        {
            _userApiManager = userApiManager;
            _connectionCache = connectionCache;
        }

        public void Login(string connectionId, string token)
        {
            using (var hashingHelper = new HashingHelper(SHA512.Create()))
            {
                var tokenData = _userApiManager.GetToken(hashingHelper.GenerateHash(token));
                _connectionCache.TryAdd(connectionId, tokenData);
            }
        }

        public void Logout(string connectionId)
        {
            _connectionCache.TryRemove(connectionId);
        }
    }
}
