using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MyBlockchain.Business;

namespace MyBlockchain.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine(10.ToString("D6"));
            //var chaindataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chaindata");
            //if (!Directory.Exists(chaindataPath))
            //{
            //    Directory.CreateDirectory(chaindataPath);
            //}
            //var block = Block.CreateFirstBlock();
            ////block.Save();

            //Console.WriteLine( DateTime.UtcNow.ToString("dd-MM-yyyy"));
            if (args.Any())
            {
                switch (args[0])
                {
                    case "m":
                        var bf = new BlockchainFacade();
                        bf.TryCreateFirstBlock();
                        var lastBlock = bf.Sync().Last();
                        var newBlock = new MiningFacade().Mine(lastBlock);
                        BlockchainFacade.SaveBlock(newBlock);
                        break;
                }
            }

            Console.ReadKey(false);
        }
    }
}
