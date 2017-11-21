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
                var mf = new MiningFacade();
                var bf = new BlockFacade();
                switch (args[0])
                {
                    case "m":
                        
                        //bf.TryCreateFirstBlock();
                        //var lastBlock = bf.Sync().Last();
                        //var newBlock = mf.Mine(lastBlock);
                        //BlockFacade.SaveBlock(newBlock);
                        break;
                    case "td":
                        var firstBlock = Block.CreateFirstBlock();
                        MiningFacade.MineHashAndNonce(firstBlock);
                        Console.WriteLine(firstBlock.ToJson());
                        var lastBlock = firstBlock;
                        for (int i = 0; i < 5; i++)
                        {
                            var block = mf.Mine(lastBlock);
                            Console.WriteLine(block.ToJson());
                            lastBlock = block;
                        }
                        Console.WriteLine("Done");
                        break;
                }
            }
            
            Console.ReadKey(false);
        }
    }
}
