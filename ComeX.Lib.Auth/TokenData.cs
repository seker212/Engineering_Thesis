using System;
using System.Collections.Generic;

#nullable enable

namespace ComeX.Lib.Auth
{
    /// <summary>
    /// Class that represents all data about token used for authorisation.
    /// </summary>
    public class TokenData : ICredentials
    {
        internal TokenData(string tokenHash, string userId, string username, DateTime validTo, List<string> connectionIds) 
        {
            TokenHash = tokenHash;
            UserId = userId;
            Username = username;
            ValidTo = validTo;
            _connectionIds = connectionIds;
        }

        internal TokenData(string tokenHash, string userId, string username, DateTime validTo, string connectionId) 
            : this(tokenHash, userId, username, validTo, new List<string>() { connectionId }) { }

        private readonly List<string> _connectionIds;
        internal string TokenHash { get; }
        internal DateTime ValidTo { get; }
        
        /// <summary>
        /// Id of a user who uses this token.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Username of a user who uses this token.
        /// </summary>
        public string Username { get; }
        
        /// <summary>
        /// List of all current connections' ids for this token.
        /// </summary>
        public IReadOnlyList<string> ConnectionIds { get => _connectionIds; }

        internal void AddConnectionId(string connectionId) => _connectionIds.Add(connectionId);

        internal void RemoveConnectionId(string connectionId) => _connectionIds.Remove(connectionId);

        public bool IsValid()
            => DateTime.Compare(DateTime.Now, ValidTo) < 0;


        public override int GetHashCode()
        {
            return HashCode.Combine(TokenHash, UserId, Username, ValidTo);
        }

        public override bool Equals(object? obj)
        {
            return obj is TokenData data &&
                   TokenHash == data.TokenHash &&
                   Username == data.Username &&
                   UserId == data.UserId &&
                   ValidTo == data.ValidTo;
        }
    }
}
