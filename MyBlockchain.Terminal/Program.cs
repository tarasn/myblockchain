using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlockchain.Business;

namespace MyBlockchain.Terminal
{
    class Program
    {
        private static readonly IDictionary<string, Action<string>> Commands;

        static Program()
        {
            Commands = new Dictionary<string, Action<string>>
            {
                {"h",PrintHelp },
                {"mb",MineBlock },
            };
        }

        private static void MineBlock(string obj)
        {
            var bf = new BlockFacade();
            var firstBlockCreated = new Genesis().TryGenerateFirstBlock();
            

        }

        private static void PrintHelp(string obj)
        {
            Console.WriteLine("h-print this help");
            Console.WriteLine("mb-mine block");
        }

        static void Main(string[] args)
        {
            var run = true;
            Console.WriteLine("Enter a command(h-help, q-quit):");
            while (run)
            {
                var cmd = Console.ReadLine();
                if (cmd.ToLower() == "q")
                    run = false;
                else
                    ExecuteCommand(cmd);
            }
        }

        private static void ExecuteCommand(string cmd)
        {
            if (Commands.ContainsKey(cmd))
                Commands[cmd](string.Empty);
            else
                Console.WriteLine($"{cmd} not exists");
        }
    }
}
