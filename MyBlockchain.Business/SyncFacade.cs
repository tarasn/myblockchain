using System;
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
       

        

        public Chain SyncLocal()
        {
            var chain = new Chain(Enumerable.Empty<Block>());
            var serializer = new JavaScriptSerializer();
            if (Directory.Exists(DataPath))
            {
                foreach (var filepath in Directory.EnumerateFiles(DataPath, "*.json"))
                {
                    var json = File.ReadAllText(filepath);
                    var block = serializer.Deserialize<Block>(json);
                    chain.AddBlock(block);
                }
            }
            return chain;
        }

        public Chain SyncOverall()
        {
            var bestChain = SyncLocal();
            var nodes = ReadNodes();
            foreach (var node in nodes)
            {
                
            }
            throw new NotImplementedException();
        }
    }
}
