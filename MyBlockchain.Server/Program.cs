using System;
using System.Linq;
using Nancy.Hosting.Self;

namespace MyBlockchain.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 5000;
            if (args != null && args.Any())
            {
                int.TryParse(args[0], out port);
            }
            using (var host = new NancyHost(new Uri($"http://127.0.0.1:{port}")))
            {
                host.Start();
                Console.WriteLine($"Running on http://127.0.0.1:{port}");
                Console.ReadLine();
            }
        }
    }
}
