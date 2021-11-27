using ComeX.Lib.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComeX.Lib.Auth
{
    public class LoginManager
    {
        private IUserApiManager _userApiManager;
        private ConnectionCache _connectionCache;

        internal LoginManager(IUserApiManager userApiManager, ConnectionCache connectionCache)
        {
            _userApiManager = userApiManager;
            _connectionCache = connectionCache;
        }

        public LoginManager(ConnectionCache connectionCache)
            : this(new UserApiManager(), connectionCache) { }

        public void Login(string connectionId, string token)
        {
            using (var hashingHelper = new HashingHelper(SHA512.Create()))
            {
                var tokenHash = hashingHelper.GenerateHash(token);
                var tokenMessage = _userApiManager.GetToken(tokenHash);
                var tokenData = new TokenData(tokenHash, tokenMessage.Username, DateTime.Parse(tokenMessage.ValidTo), connectionId);
                if (tokenData.IsValid())
                   if (!_connectionCache.TryAdd(connectionId, tokenData))
                        throw new Exception(); //FIXME: Change exception
                else
                    throw new Exception(); //FIXME: Change exception
            }
        }

        public void Logout(string connectionId)
        {
            _connectionCache.TryRemove(connectionId);
        }
    }
}
