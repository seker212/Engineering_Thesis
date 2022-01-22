using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comex.Server.AdminCli
{
    public class CommandHandler
    {
        public readonly string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? throw new ArgumentNullException("Environment variable CONNECTION_STRING was not set.");
        const string HELP_TEXT = @"
Available commands:
    setup                           Sets up connected database to be used with server. This should be run during setting up the server for the first time.
    room mk <<room-name>>           Adds new text room of a given room name to the server as an unarchived room. Example: ""room mk room1""
    room ar <<room-name>>           Marks the unarchived room with a given room name as an archived room. Example: ""room ar room1""
    room unar <<room-name>>         Marks the room with a given room name as an unarchived room. Example: ""room unar room1""
    whitelist add <<user-name>>     Adds the user with a given user name to the server's whitelist. Example: ""whitelist add user1""
    whitelist rm <<user-name>>      Removes the user with a given user name from the server's whitelist. Example: ""whitelist rm user1""
";

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
                    case "setup":
                        new SetupCommandHandler(connectionString).HandleSetupCommand();
                        break;
                    case "help":
                    case "man":
                        Console.WriteLine(HELP_TEXT);
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
