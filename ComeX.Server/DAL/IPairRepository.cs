using ComeX.Server.DatabaseModels;
using System;
using System.Collections.Generic;

namespace ComeX.Server.DAL
{
    public interface IPairRepository<T> : IRepository<T> where T : IDbModel
    {
        public bool Delete(Guid primaryKey1, Guid primaryKey2);
        T Get(Guid primaryKey1, Guid primaryKey2);
        T Get(IEnumerable<Guid> primaryKeys);
    }
}