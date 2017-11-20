using System;
using System.IO;
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
            var chaindataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chaindata");
            if (!Directory.Exists(chaindataPath))
            {
                Directory.CreateDirectory(chaindataPath);
            }
            var block = Block.CreateFirstBlock();
            //block.Save();

            Console.WriteLine( DateTime.UtcNow.ToString("dd-MM-yyyy"));
            Console.ReadKey(false);
        }
    }
}
