using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using MyBlockchain.Business;
using NUnit.Framework;

namespace MyBlockchain.Tests
{
    [TestFixture]
    public class BlockShould
    {
        readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();

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
            var block = new Block();
            block.Index = 123;
            block.Timestamp = "12-24-2017";
            block.PrevHash = "123-prevhash-123";
            block.Hash = "123-hash-123";
            block.Data = "some data";
            block.Nonce = "1232";
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
            var json = block.ToJson();
            var desBlock = _serializer.Deserialize<Block>(json);
            Assert.IsTrue(Block.BlockComparer.Equals(block,desBlock));
        }
    }
}
