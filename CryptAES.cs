using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApplication1
{
    class CryptAES
    {
        static void Main(string[] args)
        {
            string key = "cly123zhang58s@d";
            Console.WriteLine(Encrypt("我真的是一个好人", key));
            Console.WriteLine(Decrypt("nsbOEGPPo6x0P/0kDNZ6ScBfl2a2zcNdwFPkJ2thYFU=", key));
            Console.ReadLine();
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="text">要加密的文本</param>
        /// <param name="key">密钥</param>
        /// <returns>返回加密后的密文</returns>
        public static string Encrypt(string text, string key)
        {
            byte[] textBytes = Encoding.UTF8.GetBytes(text);
            string iv = "";
            RijndaelManaged rijndael = SetRijndaelManaged(key, iv);
            ICryptoTransform transform = rijndael.CreateEncryptor();
            byte[] encryptBytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
            string newText = Convert.ToBase64String(encryptBytes);
            return newText;
        }

        /// <summary>
        /// 初始化RijndaelManaged
        /// </summary>
        /// <param name="key">密钥</param>
        /// <param name="iv">向量</param>
        /// <returns></returns>
        private static RijndaelManaged SetRijndaelManaged(string key, string iv)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged
            {
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128
            };
            byte[] keyBytes = new byte[16];
            byte[] oldKey = Encoding.UTF8.GetBytes(key);
            int len = oldKey.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(oldKey, keyBytes, len);
            rijndaelManaged.Key = keyBytes;
            byte[] ivBytes = new byte[16];
            byte[] oldiv = Encoding.UTF8.GetBytes(iv);
            int ivlen = oldiv.Length;
            if (ivlen > ivBytes.Length)
            {
                ivlen = ivBytes.Length;
            }
            Array.Copy(oldiv, ivBytes, ivlen);
            rijndaelManaged.IV = ivBytes;
            return rijndaelManaged;
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Decrypt(string text, string password)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.ECB;
            rijndaelCipher.Padding = PaddingMode.PKCS7;
            rijndaelCipher.KeySize = 128;
            rijndaelCipher.BlockSize = 128;
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] keyBytes = new byte[16];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length) len = keyBytes.Length;
            System.Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            //byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(iv);
            //rijndaelCipher.IV = ivBytes;
            ICryptoTransform transform = rijndaelCipher.CreateDecryptor();
            byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);

        }
    }

}
