using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimplHelpers
{
    public static class SecurityHelpers
    {
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("XgqO4laWhj8q1fyp");
        private const int keysize = 256;

        public static string ComputeMd5Hash(this string plainText) 
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(plainText);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
                // without dashes
               .Replace("-", string.Empty)
                // make lowercase
               .ToLower();

            return encoded;
        }


        public static string ComputeSha256Hash(this string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] bytes1 = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, (byte[])null))
            {
                byte[] bytes2 = passwordDeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes2, SecurityHelpers.initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(bytes1, 0, bytes1.Length);
                                cryptoStream.FlushFinalBlock();
                                return Convert.ToBase64String(memoryStream.ToArray());
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(passPhrase, (byte[])null))
            {
                byte[] bytes = passwordDeriveBytes.GetBytes(32);
                using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
                {
                    rijndaelManaged.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes, SecurityHelpers.initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(buffer))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] numArray = new byte[buffer.Length];
                                int count = cryptoStream.Read(numArray, 0, numArray.Length);
                                return Encoding.UTF8.GetString(numArray, 0, count);
                            }
                        }
                    }
                }
            }
        }
    }
}
