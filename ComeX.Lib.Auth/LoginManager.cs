using ComeX.Lib.Common.Helpers;
using ComeX.Lib.Common.UserDatabaseAPI;
using System;
using System.Net;
using System.Security.Cryptography;

namespace ComeX.Lib.Auth
{
    public class LoginManager : ILoginManager
    {
        private readonly IUserApiManager _userApiManager;
        private readonly ConnectionCache _connectionCache;

        internal LoginManager(IUserApiManager userApiManager, ConnectionCache connectionCache)
        {
            _userApiManager = userApiManager;
            _connectionCache = connectionCache;
        }

        public void Login(string connectionId, string token)
        {
            string tokenHash;
            TokenDataModel tokenReceived;
            using (var hashingHelper = new HashingHelper(SHA512.Create()))
            {
                tokenHash = hashingHelper.GenerateHash(token);
            }
            try
            {
                tokenReceived = _userApiManager.GetToken(tokenHash);
            }
            catch (UserApiException ex) when (ex.ResponseStatusCode.HasValue && ex.ResponseStatusCode.Value == HttpStatusCode.Unauthorized)
            {
                throw new InvalidCredentialsException("Invalid token.");
            }
            var tokenData = new TokenData(tokenHash, tokenReceived.UserId, tokenReceived.Username, DateTime.Parse(tokenReceived.ValidTo), connectionId);
            if (tokenData.IsValid())
            {
                if (!_connectionCache.TryAdd(token, tokenData))
                    throw new InvalidCredentialsException("Could not add token to connection cache.");
            }
            else
                throw new InvalidCredentialsException("Invalid token.");
        }

        public void Logout(string connectionId)
        {
            _connectionCache.TryRemove(connectionId);
        }
    }
}
