using ComeX.Server.DatabaseModels;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public class PairRepository<T> : Repository<T>, IPairRepository<T> where T : IDbModel
    {
        public PairRepository(string connectionString, string tableName, string[] columnNames) : base(connectionString, tableName, columnNames)
        {
        }

        public T Get(Guid primaryKey1, Guid primaryKey2) => Query().Where(_columnNames[0], primaryKey1).Where(_columnNames[1], primaryKey2).First<T>();

        public T Get(IEnumerable<Guid> primaryKeys) => primaryKeys.Count() == 2 ? Query().Where(_columnNames[0], primaryKeys.ElementAt(0)).Where(_columnNames[1], primaryKeys.ElementAt(1)).First<T>() : throw new ArgumentException();

        public override T Get(object primaryKey)
        {
            var primaryKeys = primaryKey as IEnumerable<Guid> ?? throw new ArgumentNullException();
            return Get(primaryKeys);
        }

        public override T Update(T entity) => Query().Where(_columnNames[0], entity.Data[0]).Where(_columnNames[1], entity.Data[1]).Update(GenerateDataDictionary(entity, 2)) == 1 ? entity : throw new Exception();
        public override bool Delete(T entity) => Query().Where(_columnNames[0], entity.Data[0]).Where(_columnNames[1], entity.Data[1]).Delete() == 1 ? true : throw new Exception();
        public bool Delete(Guid primaryKey1, Guid primaryKey2) => Query().Where(_columnNames[0], primaryKey1).Where(_columnNames[1], primaryKey2).Delete() == 1 ? true : throw new Exception();
    }
}
