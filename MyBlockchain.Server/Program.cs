using System;
using System.Web.Script.Serialization;
using MyBlockchain.Business;
using Nancy;
using Nancy.Hosting.Self;

namespace MyBlockchain.Server
{
    public class BlockchainModule : NancyModule
    {
        public BlockchainModule(BlockchainFacade facade)
        {
            facade.TryCreateFirstBlock();

            Get["/blockchain"] = _ =>
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
            using (var host = new NancyHost(new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1234");
                Console.ReadLine();
            }
        }
    }
}
