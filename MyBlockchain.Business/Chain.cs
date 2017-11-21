using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlockchain.Business
{
    public class Chain
    {
        readonly List<Block> _blocks;

        public Chain(IEnumerable<Block> blocks)
        {
            _blocks = new List<Block>(blocks);
        }

        public bool IsValid()
        {
            for (int i = 1; i < _blocks.Count; i++)
            {
                var prevBlock = _blocks[i - 1];
                if (prevBlock.Index+1!=_blocks[i].Index)
                    return false;               
                if(!_blocks[i].IsValid())
                    return false;
                if(prevBlock.Hash!= _blocks[i].PrevHash)
                    return false;
            }
            return true;
        }

       

        public Block FindBlockByIndex(int index)
        {
            if (index < _blocks.Count)
                return _blocks[index];
            return null;
        }

        public Block FindBlockByHash(string hash)
        {
            foreach (var block in _blocks)
            {
                if (block.Hash == hash)
                    return block;
            }
            return null;
        }

        public int MaxIndex()
        {
            return _blocks.Last().Index;
        }

        public void AddBlock(Block block)
        {
            throw new NotImplementedException();
        }


        public void Save(TextWriter textWriter)
        {
            throw new NotImplementedException();
        }

    }
}
