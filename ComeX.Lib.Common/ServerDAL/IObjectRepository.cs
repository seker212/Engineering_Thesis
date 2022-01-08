using ComeX.Lib.Common.ServerDAL.DatabaseModels;
using System;

namespace ComeX.Lib.Common.ServerDAL
{
    public interface IObjectRepository<T> : IRepository<T> where T : IDbModel
    {
        T Get(Guid primaryKey);
        T Get(string primaryKeyGuid);
    }
}