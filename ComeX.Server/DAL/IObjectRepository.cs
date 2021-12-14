using ComeX.Server.DatabaseModels;
using System;

namespace ComeX.Server.DAL
{
    public interface IObjectRepository<T> : IRepository<T> where T : IDbModel
    {
        T Get(Guid primaryKey);
        T Get(string primaryKeyGuid);
    }
}