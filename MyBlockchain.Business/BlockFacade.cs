using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class BlockFacade
    {
        public static string DataPath
        {
            get
            {
                return 
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.BlocksFolder);
            }
        }


       

        public static void SaveBlock(Block block)
        {
            var filename = $"{block.Index.ToString("D6")}.json";
            var pathname = Path.Combine(DataPath, filename);
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
