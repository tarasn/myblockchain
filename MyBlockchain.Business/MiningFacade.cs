using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlockchain.Business
{
    public class MiningFacade
    {
        public Block Mine(Block lastBlock)
        {
            var block = new Block();
            block.Index = lastBlock.Index + 1;
            block.Timestamp = DateTime.UtcNow.ToString(Block.TimestampFormat);
            block.Data = $"I block #{block.Index}";
            block.PrevHash = lastBlock.Hash;
            block.Hash = Helpers.CreateSHA256Hash(block.Header);

            return block;
        }


    }
}
