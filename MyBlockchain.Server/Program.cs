using System;
using System.Linq;
using System.Web.Script.Serialization;
using MyBlockchain.Business;
using Nancy;
using Nancy.Hosting.Self;

namespace MyBlockchain.Server
{
    public class BlockchainModule : NancyModule
    {
        public BlockchainModule(BlockFacade facade)
        {
            facade.TryCreateFirstBlock();

            Get["/blockchain.json"] = _ =>
            {
                var nodeBlocks = facade.Sync();
                var serializer = new JavaScriptSerializer();
                return serializer.Serialize(nodeBlocks);
            };
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int port = 5000;
            if (args != null && args.Any())
            {
                int.TryParse(args[0], out port);
            }
            using (var host = new NancyHost(new Uri($"http://localhost:{port}")))
            {
                host.Start();
                Console.WriteLine($"Running on http://localhost:{port}");
                Console.ReadLine();
            }
        }
    }
}
