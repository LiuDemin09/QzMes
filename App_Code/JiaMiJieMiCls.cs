using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QRCodeGenerate
{
    public class JiaMiJieMiCls
    {
        //密钥向量
        private static byte[] Keys = { 0xA1, 0xD2, 0xB3, 0xC4, 0xF5, 0x6D, 0x7A, 0x89 };

        /// <summary>
        /// 默认密钥，8个字符，少了不行，多了取前8个
        /// </summary>
        private static string _strPassKey = "qzMES017";

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDes(string decryptString)
        {
            
            try
            {
                //byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbKey = Encoding.UTF8.GetBytes(_strPassKey.Substring(0, 8));
                byte[] rgbIv = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }



        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDes(string encryptString)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(_strPassKey.Substring(0, 8));
                byte[] rgbIv = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCsp = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIv), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        public static string String2Unicode(string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

    }
}
