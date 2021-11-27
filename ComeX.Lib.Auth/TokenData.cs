using System;
using System.Collections.Generic;

#nullable enable

namespace ComeX.Lib.Auth
{
    public class TokenData : ICredentials
    {
        internal TokenData(string tokenHash, string username, DateTime validTo, List<string> connectionIds) 
        {
            TokenHash = tokenHash;
            Username = username;
            ValidTo = validTo;
            _connectionIds = connectionIds;
        }

        internal TokenData(string tokenHash, string username, DateTime validTo, string connectionId) 
            : this(tokenHash, username, validTo, new List<string>() { connectionId }) { }

        private List<string> _connectionIds;

        internal string TokenHash { get; }
        internal DateTime ValidTo { get; }
        public string UserId { get; }
        public string Username { get; }
        public IReadOnlyList<string> ConnectionIds { get => _connectionIds; }

        internal void AddConnectionId(string connectionId) => _connectionIds.Add(connectionId);

        internal void RemoveConnectionId(string connectionId) => _connectionIds.Remove(connectionId);

        public bool IsValid()
            => DateTime.Compare(DateTime.Now, ValidTo) < 0;


        public override int GetHashCode()
        {
            return HashCode.Combine(TokenHash, Username, ValidTo);
        }

        public override bool Equals(object? obj)
        {
            return obj is TokenData data &&
                   TokenHash == data.TokenHash &&
                   Username == data.Username &&
                   ValidTo == data.ValidTo;
        }

        public static bool operator ==(TokenData? left, TokenData? right)
        {
            return EqualityComparer<TokenData>.Default.Equals(left, right);
        }

        public static bool operator !=(TokenData? left, TokenData? right)
        {
            return !(left == right);
        }
    }
}
