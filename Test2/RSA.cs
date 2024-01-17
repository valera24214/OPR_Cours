using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Test2
{
    internal class RSA
    {
        private static RSACryptoServiceProvider csp;
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;


        public RSA()
        {
            csp = new RSACryptoServiceProvider(2048);
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }

        public string GetPublicKey()
        {
            return JsonConvert.SerializeObject(_publicKey);
        }

        public string Encrypt(RSAParameters _publicKey_left, string Text)
        {
            csp.ImportParameters(_publicKey_left);
            var data = Encoding.Unicode.GetBytes(Text);
            var cypher = csp.Encrypt(data, false);
            return Convert.ToBase64String(cypher);
        }

        public string Decrypt(string cypher)
        {
            var dataBytes = Convert.FromBase64String(cypher);
            csp.ImportParameters(_privateKey);
            var plain = csp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plain);
        }
    }
}
