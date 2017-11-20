using System;

namespace MyBlockchain.Business
{
    public class MiningFacade
    {
        private const int NumZeros = 4;
        public Block Mine(Block lastBlock)
        {
            var block = new Block();
            block.Index = lastBlock.Index + 1;
            
            block.Data = $"I block #{block.Index}";
            block.PrevHash = lastBlock.Hash;

            var zeroPrefix = string.Empty.PadRight(NumZeros, '0');
            block.Hash = Helpers.CreateSHA256Hash(block.Header);
            while (!block.Hash.StartsWith(zeroPrefix))
            {
                block.Nonce++;
                block.Hash = Helpers.CreateSHA256Hash(block.Header);
            }
            block.Timestamp = DateTime.UtcNow.ToString(Block.TimestampFormat);
            return block;
        }


    }
}
