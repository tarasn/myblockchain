using System.Web.Script.Serialization;
using MyBlockchain.Business;
using NUnit.Framework;

namespace MyBlockchain.Tests
{
    [TestFixture]
    public class ChainShould
    {
        readonly JavaScriptSerializer _serializer = new JavaScriptSerializer();
        private Block _block0;
        private Block _block1;
        private Block _block2;
        private Block _block3;

        [SetUp]
        public void Setup()
        {
            _block0 = new Block("{ \"Index\":0,\"Timestamp\":1511283397,\"PrevHash\":\"\",\"Hash\":\"000004982af96db60e89bb3b0621eaeef4448f9359d4ff0ac8db2414b9a14a63\",\"Data\":\"First block data\",\"Nonce\":321401,\"Header\":\"0First block data1511283397321401\"}");
            _block1 = new Block("{ \"Index\":1,\"Timestamp\":1511283402,\"PrevHash\":\"000004982af96db60e89bb3b0621eaeef4448f9359d4ff0ac8db2414b9a14a63\",\"Hash\":\"0000066177283925c98dac8a010d14afcbbaed17d32a4293fafddef49d79f5c2\",\"Data\":\"I block #1\",\"Nonce\":93766,\"Header\":\"1000004982af96db60e89bb3b0621eaeef4448f9359d4ff0ac8db2414b9a14a63I block #1151128340293766\"}");
            _block2 = new Block("{ \"Index\":2,\"Timestamp\":1511283403,\"PrevHash\":\"0000066177283925c98dac8a010d14afcbbaed17d32a4293fafddef49d79f5c2\",\"Hash\":\"00000790baca1cf65a0361369eaa9d21543eaa65e3addbfb76fae7fc71bd618f\",\"Data\":\"I block #2\",\"Nonce\":3631736,\"Header\":\"20000066177283925c98dac8a010d14afcbbaed17d32a4293fafddef49d79f5c2I block #215112834033631736\"}");
            _block3 = new Block("{ \"Index\":3,\"Timestamp\":1511283461,\"PrevHash\":\"00000790baca1cf65a0361369eaa9d21543eaa65e3addbfb76fae7fc71bd618f\",\"Hash\":\"000000d7a946beb3ed40cb6b36748c7d16bab9f32fc46b044aa6618a77bdb454\",\"Data\":\"I block #3\",\"Nonce\":1516110,\"Header\":\"300000790baca1cf65a0361369eaa9d21543eaa65e3addbfb76fae7fc71bd618fI block #315112834611516110\"}");
        }

        [Test]
        public void ReturnFalseWhenBlockNotIndexedAfterOther()
        {
            var chain = new Chain(new [] {_block2,_block1});
            Assert.IsFalse(chain.IsValid());
        }

        [Test]
        public void ReturnTrueWhenBlockIndexedAfterOther()
        {
            var chain = new Chain(new[] { _block1, _block2 });
            Assert.IsTrue(chain.IsValid());
        }

        [Test]
        public void ReturnTrueWhenChainLargerThanAnother()
        {
            var c0 = new Chain(new [] {_block0,_block1,_block2});
            var c1 = new Chain(new [] {_block0,_block1});
            Assert.IsTrue(c0>c1);
        }

        [Test]
        public void ReturnFalseWhenChainIsLessThanAnother()
        {
            var c0 = new Chain(new[] { _block0, _block1, _block2 });
            var c1 = new Chain(new[] { _block0, _block1 });
            Assert.IsFalse(c0 < c1);
        }
    }
}
