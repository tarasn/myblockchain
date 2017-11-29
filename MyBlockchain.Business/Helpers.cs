using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace MyBlockchain.Business
{
    public static class Helpers
    {
        private static readonly SHA256CryptoServiceProvider CryptoServiceProvider;

        static Helpers()
        {
            CryptoServiceProvider = new SHA256CryptoServiceProvider();
        }

        public static string CreateSHA256Hash(string text)
        {
            var hashBytes = CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("x2"));
            }
            return sb.ToString();
        }

        public static string DownloadString(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string data = string.Empty;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return data;
        }
    }
}
