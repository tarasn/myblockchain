using System;
using System.Collections.Generic;
using MyBlockchain.Business;
using NUnit.Framework;

namespace MyBlockchain.Tests
{
    [TestFixture]
    public class BlockShould
    {
       

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreatedFromDictionary()
        {
            var block = CreateBlock();
            Assert.AreEqual(block.Index,123);
            Assert.AreEqual(block.Timestamp, "12-24-2017");
            Assert.AreEqual(block.PrevHash, "123-prevhash-123");
            Assert.AreEqual(block.Hash, "123-hash-123");
            Assert.AreEqual(block.Data, "some data");
            Assert.AreEqual(block.Nonce, "1232");
        }

        private static Block CreateBlock()
        {
            var dic = new Dictionary<string, string>
            {
                {"index", "123"},
                {"timestamp", "12-24-2017"},
                {"prevhash", "123-prevhash-123"},
                {"hash", "123-hash-123"},
                {"data", "some data"},
                {"nonce", "1232"},
            };
            var block = new Block(dic);
            return block;
        }

        [Test]
        public void CreateFirstBlock()
        {
            var block = Block.CreateFirstBlock();
            Assert.AreEqual(block.Index, 0);
            Assert.AreEqual(block.Timestamp, DateTime.UtcNow.ToString(Block.TimestampFormat));
            Assert.AreEqual(block.PrevHash, string.Empty);
            Assert.AreEqual(block.Data, "First block data");
            Assert.AreEqual(block.Nonce, "0");
        }

        [Test]
        public void CreateProperlyFormatedHeader()
        {
            var block = CreateBlock();
            Assert.AreEqual(block.Header, $"{block.Index}{block.PrevHash}{block.Data}{block.Timestamp}{block.Nonce}");
        }

        [Test]
        public void ConvertToDictionary()
        {
            var block = CreateBlock();
            var dic = block.ToDictionary();
            Assert.AreEqual(Convert.ToInt32(dic["index"]), block.Index);
            Assert.AreEqual(dic["timestamp"], block.Timestamp);
            Assert.AreEqual(dic["prevhash"], block.PrevHash);
            Assert.AreEqual(dic["hash"], block.Hash);
            Assert.AreEqual(dic["data"], block.Data);
            Assert.AreEqual(dic["nonce"], block.Nonce);
        }
    }
}
