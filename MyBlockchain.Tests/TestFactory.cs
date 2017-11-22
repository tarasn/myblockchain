using System;
using MyBlockchain.Business;

namespace MyBlockchain.Tests
{
    static class TestFactory
    {
        public static Block CreateBlock(int index=123,long timestamp= 123456,
            string prevHash= "123-prevhash-123", string hash = "123-prevhash-123",
            string data= "some data", int nonce=1232)
        {
            var block = new Block
            {
                Index = index,
                Timestamp = timestamp,
                PrevHash = prevHash,
                Hash = hash,
                Data = data,
                Nonce = nonce
            };
            return block;
        }
    }
}