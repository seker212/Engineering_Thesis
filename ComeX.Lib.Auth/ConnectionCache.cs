using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ComeX.Lib.Auth
{
    public class ConnectionCache : IConnectionCache
    {
        private readonly ConcurrentDictionary<string, TokenData> _cacheDict;

        public IEnumerable<string> Keys => _cacheDict.Keys;

        public IEnumerable<TokenData> Values => _cacheDict.Values;

        public int Count => _cacheDict.Count;

        public TokenData this[string key] => _cacheDict[key].IsValid() ? _cacheDict[key] : throw new InvalidCredentialsException("Token has expired");

        internal ConnectionCache()
        {
            _cacheDict = new ConcurrentDictionary<string, TokenData>();
        }

        internal bool TryAdd(string token, TokenData tokenData)
        {
            if (_cacheDict.Values.Contains(tokenData))
            {
                var existingTokenData = _cacheDict.First(x => x.Value.Equals(tokenData)).Value;
                existingTokenData.AddConnectionId(token);
                return _cacheDict.TryAdd(token, existingTokenData);
            }
            else
            {
                if (tokenData.ConnectionIds.Count == 0)
                    tokenData.AddConnectionId(token);
                return _cacheDict.TryAdd(token, tokenData);
            }
        }

        internal bool TryRemove(string connectionId) => _cacheDict.TryRemove(connectionId, out _);

        public bool ContainsKey(string key) => _cacheDict.ContainsKey(key);

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out TokenData value)
        {
            _cacheDict.TryGetValue(key, out value);
            return value.IsValid() ? true : throw new InvalidCredentialsException("Token has expired");
        }

        public IEnumerator<KeyValuePair<string, TokenData>> GetEnumerator() => _cacheDict.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _cacheDict.GetEnumerator();
    }
}
