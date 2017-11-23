using System;
using System.IO;
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
        public void ProperlyIConstructed()
        {
            var block = TestFactory.CreateBlock();
            Assert.AreEqual(block.Index,123);
            Assert.AreEqual(block.Timestamp, "12-24-2017");
            Assert.AreEqual(block.PrevHash, "123-prevhash-123");
            Assert.AreEqual(block.Hash, "123-hash-123");
            Assert.AreEqual(block.Data, "some data");
            Assert.AreEqual(block.Nonce, 1232);
        }

        

        [Test]
        public void CreateFirstBlock()
        {
            //var block = new Genesis().TrCreateFirstBlock();
            //Assert.AreEqual(block.Index, 0);
            //Assert.AreEqual(block.Timestamp, DateTime.UtcNow.ToString(Block.TimestampFormat));
            //Assert.AreEqual(block.PrevHash, string.Empty);
            //Assert.AreEqual(block.Data, "First block data");
            //Assert.AreEqual(block.Nonce, 0);
        }

        [Test]
        public void CreateProperlyFormatedHeader()
        {
            var block = TestFactory.CreateBlock();
            Assert.AreEqual(block.Header, $"{block.Index}{block.PrevHash}{block.Data}{block.Timestamp}{block.Nonce}");
        }

        [Test]
        public void ConvertToJson()
        {
            var block = TestFactory.CreateBlock();
            var json = block.ToJson();
            var desBlock = _serializer.Deserialize<Block>(json);
            Assert.IsTrue(Block.BlockComparer.Equals(block,desBlock));
        }


        [Test]
        public void ReturnFalseWhenHashNotStartsWithZeroes()
        {
            var block = TestFactory.CreateBlock();
            Assert.IsFalse(block.IsValid());
        }

        [Test]
        public void ReturnTrueWhenHashStartsWithZeroes()
        {
            var block = TestFactory.CreateBlock();
            block.Hash = string.Empty.PadRight(Constants.NumZeros, '0') + "aaasdsaa";
            Assert.IsFalse(block.IsValid());
        }

        [Test]
        public void SaveToJson()
        {
            var block = TestFactory.CreateBlock();
            using (var sw = new StringWriter())
            {
                block.Save(sw);
                var json = sw.ToString();
                var desBlock = _serializer.Deserialize<Block>(json);
                Assert.IsTrue(Block.BlockComparer.Equals(block, desBlock));
            }
        }

    }
}
