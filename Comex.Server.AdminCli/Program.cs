using System;

namespace Comex.Server.AdminCli
{
    class Program
    {
        static void Main(string[] args)
        {
            RunCli();
        }

        static void RunCli()
        {
            var c = new CommandHandler();
            while (true)
            {
                Console.Write("> ");
                c.HandleCommand(Console.ReadLine());
            }
        }
    }
}
