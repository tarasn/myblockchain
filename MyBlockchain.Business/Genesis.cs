using System;
using System.IO;
using System.Linq;

namespace MyBlockchain.Business
{
    public class Genesis
    {
        public Block GenerateFirstBlockIfNotExist()
        {
            if (!Directory.Exists(SyncFacade.DataPath))
            {
                Directory.CreateDirectory(SyncFacade.DataPath);
            }
            if (!Directory.EnumerateFiles(SyncFacade.DataPath, "*.json").Any())
            {
                var firstBlock = new Block
                {
                    Index = 0,
                    Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
                    Data = "First block data",
                    Hash = string.Empty,
                    PrevHash = string.Empty,
                    Nonce = 0
                };
                MiningFacade.FindValidNonce(firstBlock);
                Block.SaveBlock(firstBlock);
            }
            return null;
        }
    }
}
