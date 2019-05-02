using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BankingApp
{
    public class HashSalt
    {
        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string GenerateSalt(int length)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[length];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sha256hashstring = new SHA256Managed();
            byte[] hash = sha256hashstring.ComputeHash(bytes);

            return ByteArrayToString(hash);
        }

    }
}