﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlockchain.Business
{
    public class Genesis
    {
        public bool TryGenerateFirstBlock()
        {
            if (!Directory.Exists(BlockFacade.DataPath))
            {
                Directory.CreateDirectory(BlockFacade.DataPath);
            }
            if (!Directory.EnumerateFiles(BlockFacade.DataPath, "*.json").Any())
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
                BlockFacade.SaveBlock(firstBlock);
                return true;
            }
            Console.WriteLine("Chaindata dir already exists with blocks.\nIf you want to regenerate the blocks, delete /chaindata and rerun");
            return false;
        }
    }
}