using ComeX.Server.DatabaseModels;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DAL
{
    public abstract class Repository<T> : IDisposable, IRepository<T> where T : IDbModel
    {
        private readonly DbConnection _connection;
        private readonly QueryFactory _queryFactory;
        private readonly string _tableName;
        protected readonly string[] _columnNames;

        public Repository(string connectionString, string tableName, string[] columnNames)
        {
            _connection = new NpgsqlConnection(connectionString);
            _queryFactory = new QueryFactory(_connection, new PostgresCompiler());
            _tableName = tableName;
            _columnNames = columnNames;

            _queryFactory.Logger = compiled => { Debug.WriteLine(compiled.ToString()); };
        }

        public void Dispose()
        {
            _queryFactory.Dispose();
            _connection.Dispose();
        }

        protected virtual IDictionary<string, object> GenerateDataDictionary(T entity, int start)
        {
            var dict = new Dictionary<string, object>();
            for (int i = start; i < _columnNames.Length; i++)
            {
                dict.Add(_columnNames[i], entity.Data[i]);
            }
            return dict;
        }

        public IEnumerable<T> Get() => _queryFactory.Query(_tableName).Get<T>();
        protected Query Query() => _queryFactory.Query(_tableName);
        public virtual T Get(object primaryKey) => Query().Where(_columnNames[0], primaryKey).First<T>();
        public virtual T Insert(T entity) => Query().Insert(GenerateDataDictionary(entity, 0)) == 1 ? entity : throw new Exception(); // zrobić lepszy exception bo się nie połapiemy
        public virtual T Update(T entity) => Query().Where(_columnNames[0], entity.Data[0]).Update(GenerateDataDictionary(entity, 1)) == 1 ? entity : throw new Exception(); // jak wyżej
        public virtual bool Delete(T entity) => Query().Where(_columnNames[0], entity.Data[0]).Delete() == 1 ? true : throw new Exception()); // jak wyżej znowu

    }
}
