using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoreConsole
{
    public class CryptAESCore
    {
        public static void Main(string[] args)
        {

            string encryMsg = "123456789";
            //获得加密字符串
            string encryData = AesEncryptText(encryMsg);
            Console.WriteLine(encryData);
            //获得解密字符串
            Console.WriteLine(AesDecryptText(encryData));
            Console.ReadLine();
        }

        #region AES加解密
        //key16 128加密，key24 192加密，key32 256加密
        private static string key = "yunzhangcaijing$";

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesEncryptText(string input)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(key);
            byte[] bytesEncrypted = AESEncryptBytes(bytesToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesEncrypted);
            return result;
        }
        public static byte[] AESEncryptBytes(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            using (var ms = new MemoryStream())
            {
                using (var AES = Aes.Create())
                {
                    AES.KeySize = 128;
                    AES.BlockSize = 128;
                    AES.Mode = CipherMode.ECB;
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Key = passwordBytes;
                    AES.IV = new byte[16];
                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Dispose();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AesDecryptText(string input)
        {
            try
            {
                byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(key);
                byte[] bytesDecrypted = AESDecryptBytes(bytesToBeDecrypted, passwordBytes);
                string result = Encoding.UTF8.GetString(bytesDecrypted);
                return result;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }

        public static byte[] AESDecryptBytes(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            using (var ms = new MemoryStream())
            {
                using (var AES = Aes.Create())
                {
                    AES.KeySize = 128;
                    AES.BlockSize = 128;
                    AES.Mode = CipherMode.ECB;
                    AES.Padding = PaddingMode.PKCS7;
                    AES.Key = passwordBytes;
                    AES.IV = new byte[16];

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Dispose();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
        #endregion
    }
}
