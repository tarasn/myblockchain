using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class BlockchainFacade
    {
        public BlockchainFacade()
        {
        }

        public string DataPath
        {
            get
            {
                return 
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "chaindata");
            }
        }


        public void TryCreateFirstBlock()
        {
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            if (!Directory.EnumerateFiles(DataPath, "*.json").Any())
            {
                var block = Block.CreateFirstBlock();
                var filename = $"{block.Index.ToString("D6")}.json";
                var pathname = Path.Combine("chaindata", filename);
                using(StreamWriter writer = File.CreateText(pathname))
                    block.Save(writer);
            }
        }

        public List<Block> Sync()
        {
            var nodeBlocks = new List<Block>();
            var serializer = new JavaScriptSerializer();
            if (Directory.Exists(DataPath))
            {
                foreach (var filepath in Directory.EnumerateFiles(DataPath,"*.json"))
                {
                    var json = File.ReadAllText(filepath);
                    var block = serializer.Deserialize<Block>(json);
                    nodeBlocks.Add(block);
                }
            }
            return nodeBlocks;
        }
    }
}
