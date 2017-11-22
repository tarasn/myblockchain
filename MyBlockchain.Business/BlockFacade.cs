using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class BlockFacade
    {
        public string DataPath
        {
            get
            {
                return 
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.BlocksFolder);
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
                SaveBlock(block);
            }
        }

        public static void SaveBlock(Block block)
        {
            var filename = $"{block.Index.ToString("D6")}.json";
            var pathname = Path.Combine(Constants.BlocksFolder, filename);
            using (var writer = File.CreateText(pathname))
                block.Save(writer);
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
