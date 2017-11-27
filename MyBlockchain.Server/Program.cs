using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MyBlockchain.Business;
using Nancy.Hosting.Self;

namespace MyBlockchain.Server
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

        

        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            
            int port = 5000;
            if (args != null && args.Any())
            {
                int.TryParse(args[0], out port);
            }
            var httpServerTask = Task.Run(() =>
            {
                using (var host = new NancyHost(new Uri($"http://127.0.0.1:{port}")))
                {
                    host.Start();
                    Console.WriteLine($"Running on http://127.0.0.1:{port}");
                    cts.Token.WaitHandle.WaitOne(Timeout.Infinite);
                }
            });
            Thread.Sleep(TimeSpan.FromSeconds(1));
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
            cts.Cancel();
        }


        private static void MineBlock(string obj)
        {
            var bf = new BlockFacade();
            var firstBlockCreated = new Genesis().TryGenerateFirstBlock();


        }

        private static void ExecuteCommand(string cmd)
        {
            if (Commands.ContainsKey(cmd))
                Commands[cmd](string.Empty);
            else
                Console.WriteLine($"{cmd} not exists");
        }

        private static void PrintHelp(string obj)
        {
            Console.WriteLine("h-print this help");
            Console.WriteLine("mb-mine block");
        }
    }
}
