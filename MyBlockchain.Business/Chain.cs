using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class Chain
    {
        readonly List<Block> _blocks;
        readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        public Chain(IEnumerable<Block> blocks)
        {
            _blocks = new List<Block>(blocks);
        }

        public bool IsValid()
        {
            for (var i = 1; i < _blocks.Count; i++)
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
            if(block.Index==_blocks.Last().Index+1)
                _blocks.Add(block);
        }

        public Block Last()
        {
            return _blocks.LastOrDefault();
        }

        public void Save(TextWriter textWriter)
        {
            textWriter.Write(_serializer.Serialize(_blocks));
        }


        public static bool operator >(Chain c0, Chain c1)
        {
            return c0._blocks.Count > c1._blocks.Count;
        }

        public static bool operator <(Chain c0, Chain c1)
        {
            return c0._blocks.Count < c1._blocks.Count;
        }

    }
}
