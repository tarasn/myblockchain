using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace MyBlockchain.Business
{
    public class Block
    {
        public const string TimestampFormat = "yy-MM-dd";

        public Block(IDictionary<string, string> blockData)
        {
            foreach (var prop in GetType().GetProperties())
            {
                var propertyName = prop.Name.ToLower();
                if (blockData.ContainsKey(propertyName))
                {
                    switch (propertyName)
                    {
                        case "index":
                                prop.SetValue(this, Convert.ToInt32(blockData[propertyName]));
                            break;
                        default:
                                prop.SetValue(this, blockData[propertyName]);
                            break;
                    }
                }
            }
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

        public IDictionary<string, string> ToDictionary()
        {
            return GetType()
                .GetProperties()
                .ToDictionary(p => p.Name.ToLower(), p => Convert.ToString(p.GetValue(this)));
        }

        public override string ToString()
        {
            return $"Block<PrevHash: {PrevHash}, Hash: {Hash}>";
        }

        public static Block CreateFirstBlock()
        {
            var blockData = new Dictionary<string,string>
            {
                {"index","0" },
                {"timestamp",DateTime.UtcNow.ToString(TimestampFormat) },
                {"data","First block data" },
                {"prevhash",string.Empty },
                {"nonce","0" }
            };
            return new Block(blockData);
        }

        public void Save()
        {
            var filename = $"{Index.ToString("D6")}.json";
            var pathname = Path.Combine("chaindata", filename);
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(this);
            File.WriteAllText(pathname,json);
        }
    }
}