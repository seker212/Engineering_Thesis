using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comex.Server.AdminCli
{
    public class CommandHandler
    {
        string connectionString = "Server = comx.molly.ovh; Port = 25432; Database = postgres; User Id = banan; Password = s9n5#@Jo";

        public void HandleCommand(string? commandText)
        {
            try
            {
                var args = commandText.Split(' ');
                switch (args[0])
                {
                    case "room":
                        new RoomCommandHandler(connectionString).HandleRoomCommand(args.Skip(1));
                        break;
                    case "whitelist":
                        new WhitelistCommandHandler(connectionString).HandleWhitelistCommand(args.Skip(1));
                        break;
                    default:
                        throw new UnknownCommandException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
            }
        }        
    }
}
