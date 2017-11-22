using System.Security.Cryptography;
using System.Text;

namespace MyBlockchain.Business
{
    public static class Helpers
    {
        private static readonly SHA256CryptoServiceProvider _cryptoServiceProvider;

        static Helpers()
        {
            _cryptoServiceProvider = new SHA256CryptoServiceProvider();
        }

        public static string CreateSHA256Hash(string text)
        {
            var hashBytes = _cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
