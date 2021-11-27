using System;
using System.Security.Cryptography;
using System.Text;

namespace ComeX.Lib.Common.Helpers
{
    public class HashingHelper : IDisposable
    {
        private HashAlgorithm _hashAlgorithm;

        public HashingHelper(HashAlgorithm hashAlgorithm)
        {
            _hashAlgorithm = hashAlgorithm;
        }

        public void Dispose()
        {
            _hashAlgorithm.Dispose();
        }

        public string GenerateHash(string value)
        {
            var strBuilder = new StringBuilder();
            foreach (byte b in _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(value)))
                strBuilder.Append(b.ToString("X2"));
            return strBuilder.ToString();
        }
    }
}
