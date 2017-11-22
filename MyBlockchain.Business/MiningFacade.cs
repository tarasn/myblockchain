using System;

namespace MyBlockchain.Business
{
    public class MiningFacade
    {
        public Block Mine(Block lastBlock)
        {
            var block = new Block();
            block.Index = lastBlock.Index + 1;
            
            block.Data = $"I block #{block.Index}";
            block.PrevHash = lastBlock.Hash;
            block.Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            MineHashAndNonce(block);
            return block;
        }

        public static void MineHashAndNonce(Block block)
        {
            var zeroPrefix = string.Empty.PadRight(Constants.NumZeros, '0');
            block.Hash = Helpers.CreateSHA256Hash(block.Header);
            while (!block.Hash.StartsWith(zeroPrefix))
            {
                block.Nonce++;
                block.Hash = Helpers.CreateSHA256Hash(block.Header);
            }
        }
    }
}
