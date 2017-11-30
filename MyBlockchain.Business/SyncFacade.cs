using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class SyncFacade
    {

        public static string DataPath
        {
            get
            {
                return
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.BlocksFolder);
            }
        }

        private IEnumerable<string> ReadNodes()
        {
            var nodeFilePathName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                Constants.NodesFile);
            return File.ReadAllLines(nodeFilePathName);
        }

        readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        
        public Chain SyncLocal()
        {
            var chain = new Chain(Enumerable.Empty<Block>());
            if (!Directory.Exists(DataPath))
            {
                new Genesis().GenerateFirstBlockIfNotExist();
            }
            foreach (var filepath in Directory.EnumerateFiles(DataPath, "*.json"))
            {
                var json = File.ReadAllText(filepath);
                var block = _serializer.Deserialize<Block>(json);
                chain.AddBlock(block);
            }
            return chain;
        }

        public Chain SyncOverall()
        {
            var bestChain = SyncLocal();
            var nodeUrls = ReadNodes();
            foreach (var nodeUrl in nodeUrls)
            {
                var json = Helpers.DownloadString(nodeUrl);
                var blocks = _serializer.Deserialize<IEnumerable<Block>>(json);
                var peerChain =  new Chain(blocks);
                if (peerChain.IsValid() && peerChain > bestChain)
                {
                    bestChain = peerChain;
                }
            }
            bestChain.Save();
            return bestChain;
        }
    }
}
