using System;
using System.Security.Cryptography;

namespace ComeX.UserDatabaseAPI.Helpers
{
    public class TokenGenerator
    {
        public string GenerateToken()
        {
            string token = string.Empty;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                token = Convert.ToBase64String(tokenData);
            }
            return token;
        }

        public string GenerateTokenHash(string tokenValue)
        {
            string tokenHash = string.Empty;

            using (var tokenHashingHelper = new Lib.Common.Helpers.HashingHelper(SHA512.Create()))
            {
                tokenHash = tokenHashingHelper.GenerateHash(tokenValue);
            }

            return tokenHash;
        }
    }
}
