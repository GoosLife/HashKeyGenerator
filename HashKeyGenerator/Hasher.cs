using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HashKeyGenerator
{
    internal static class Hasher
    {
        public static byte[] GenerateHash(string input, HMAC algorithm)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            byte[] hash = algorithm.ComputeHash(bytes);

            return hash;
        }

        public static bool VerifyHash(string input, byte[] hash, HMAC algorithm)
        {
            byte[] newHash = GenerateHash(input, algorithm);

            return newHash.SequenceEqual(hash);
        }

        public static string WriteHashHex(byte[] hash)
        {
            // Print the hash in hexadecimal format
            StringBuilder hex = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        public static string WriteHashASCII(byte[] hash)
        {
            // Print the hash in ASCII format
            StringBuilder ascii = new StringBuilder(hash.Length);

            foreach (byte b in hash)
            {
                ascii.Append((char)b);
            }

            return ascii.ToString();
        }
    }
}
