using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    using System;
    using System.Security.Cryptography;
    using System.Text;

namespace ConsoleApp1
{
    public class CryptoHelper
    {
        public static byte[] GenerateRandomKey(int keySize)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[keySize / 8];
                rng.GetBytes(key);
                return key;
            }
        }

        public static string ComputeHMAC(string message, byte[] key)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }
    }

}
