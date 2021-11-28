using ComeX.Lib.Common.ServerCommunicationModels;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.Server.DB
{
    public class Database
    {

        public bool SaveMessage(Message message)
        {
            using (var conn = GetConnection())
            {
                var compiler = new SqlServerCompiler();
            }
                return false;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection("Server = localhost; Database = mydatabase; User Id = postgres; Password = mysecretpassword;");
        }
    }
}
