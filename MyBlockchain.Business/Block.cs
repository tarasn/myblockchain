using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class Block
    {
        public const string TimestampFormat = "dd-MM-yyyy";
        //TODO: use Lazy
        readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

        public Block()
        {
        }
        public Block(string json)
        {
            var block = _serializer.Deserialize<Block>(json);
            Index = block.Index;
            Nonce = block.Nonce;
            Data = block.Data;
            Hash = block.Hash;
            PrevHash = block.PrevHash;
            Timestamp = block.Timestamp;
            
            if (string.IsNullOrEmpty(Hash))
            {
                Hash = CreateSelfHash();
            }
            if (string.IsNullOrEmpty(Nonce))
            {
                Nonce = "None";
            }
        }

        private string CreateSelfHash()
        {
            return Helpers.CreateSHA256Hash(Header);
        }

        public int Index { get; set; }
        public string Timestamp { get; set; }
        public string PrevHash { get; set; }
        public string Hash { get; set; }
        public string Data { get; set; }
        public string Nonce { get; set; }

        public string Header => $"{Index}{PrevHash}{Data}{Timestamp}{Nonce}";

        public string ToJson()
        {
            return _serializer.Serialize(this);
        }

        public override string ToString()
        {
            return $"Block<PrevHash: {PrevHash}, Hash: {Hash}>";
        }

        public static Block CreateFirstBlock()
        {
            var block = new Block
            {
                Index = 0,
                Timestamp = DateTime.UtcNow.ToString(TimestampFormat),
                Data = "First block data",
                Hash = string.Empty,
                PrevHash = string.Empty,
                Nonce = "0"
            };
            return block;
        }


        public void Save(TextWriter textWriter)
        {
            textWriter.Write(_serializer.Serialize(this));
            //var filename = $"{Index.ToString("D6")}.json";
            //var pathname = Path.Combine("chaindata", filename);
            //var serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(this);
            //File.WriteAllText(pathname,json);
        }

        private sealed class BlockEqualityComparer : IEqualityComparer<Block>
        {
            public bool Equals(Block x, Block y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Index == y.Index && 
                    string.Equals(x.Timestamp, y.Timestamp) && 
                    string.Equals(x.PrevHash, y.PrevHash) && 
                    string.Equals(x.Hash, y.Hash) && 
                    string.Equals(x.Data, y.Data) && 
                    string.Equals(x.Nonce, y.Nonce);
            }

            public int GetHashCode(Block obj)
            {
                unchecked
                {
                    var hashCode = obj.Index;
                    hashCode = (hashCode*397) ^ (obj.Timestamp != null ? obj.Timestamp.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.PrevHash != null ? obj.PrevHash.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.Hash != null ? obj.Hash.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.Data != null ? obj.Data.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.Nonce != null ? obj.Nonce.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<Block> BlockComparerInstance = new BlockEqualityComparer();

        public static IEqualityComparer<Block> BlockComparer
        {
            get { return BlockComparerInstance; }
        }
    }
}