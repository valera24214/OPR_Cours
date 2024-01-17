using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ClientWpf
{
    public class RSA
    {
        private static RSACryptoServiceProvider csp;
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;


        public RSA()
        {
            csp = new RSACryptoServiceProvider(4096);
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
