using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Helpers
{
    public class HashingHelper
    {
        private byte[] _salt;

        public HashingHelper()
        {
            _salt = new byte[128 / 8];
            GenerateSalt();
        }

        public string Salt { get => Convert.ToBase64String(_salt); set => _salt = Convert.FromBase64String(value); }

        public string GenerateHash(string password)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
                ));
            return hashedPassword;
        }

        private void GenerateSalt()
        {
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(_salt);
            }
        }
    }
}
