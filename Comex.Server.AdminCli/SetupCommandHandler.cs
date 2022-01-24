using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Comex.Server.AdminCli
{
    class SetupCommandHandler
    {
        string _connectionString;

        public SetupCommandHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void HandleSetupCommand()
        {
            var filePath = Path.Combine("DB", "DBScript.sql");
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = File.ReadAllText(filePath);
            cmd.ExecuteNonQuery();
        }
    }
}
