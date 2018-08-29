using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for DecryptUtil
/// </summary>
public class DecryptUtil
{
	public DecryptUtil()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string EncryptForMD5(string strPwd)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        strPwd = System.Web.HttpUtility.UrlEncode(strPwd);
        byte[] data = UTF8Encoding.UTF8.GetBytes(strPwd);
        byte[] md5data = md5.ComputeHash(data);
        md5.Clear();
        string str = "";
        str = BitConverter.ToString(md5data).Replace("-", "");
        //for (int i = 0; i < md5data.Length - 1; i++)
        //{
        //    str += md5data[i].ToString("x").PadLeft(2, '0');
        //}
        return str;
    }

    public static string EncryptForBase64(string message)
    {
        byte[] inputByteArray = Encoding.UTF8.GetBytes(message);
        string str = Convert.ToBase64String(inputByteArray);
        return str;
    }


    public static string DecryptForBase64(string message)
    {
        byte[] inputByteArray = Convert.FromBase64String(message);
        string str = Encoding.UTF8.GetString(inputByteArray);
        return str;
    }

    public static string EncodeDES(string encryptString, string encryptKey)
    {
        DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
        byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
        byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
        MemoryStream mStream = new MemoryStream();
        using (CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, new byte[8]), CryptoStreamMode.Write))
        {
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            string ret = Convert.ToBase64String(mStream.ToArray());
            mStream.Close();
            return ret;
        }
        
    }

    public static string DecodeDES(string decryptString, string decryptKey)
    {
        byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
        byte[] inputByteArray = Convert.FromBase64String(decryptString);
        DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
        MemoryStream mStream = new MemoryStream();
        using (CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, new byte[8]), CryptoStreamMode.Write))
        {
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            string ret = Encoding.UTF8.GetString(mStream.ToArray());
            cStream.Close();
            return ret;
        }
    }

    public static String GetRandomString(int length) {
		string b = "abcdefghijklmnopqrstuvwxyz0123456789";
        char[] bs = b.ToCharArray();
		Random random = new Random();
        StringBuilder sb = new StringBuilder();
		for (int i = 0; i < length; i++) {
            int number = random.Next(bs.Length);
            sb.Append(bs[number]);			
		}		
		return sb.ToString();
	}

    public static string PaddingLeft(char c, int length, string content)
    {
        string str = "";
        string cs = "";
        if (content.Length > length)
        {
            str = content;
        }
        else
        {
            for (int i = 0; i < length - content.Length; i++)
            {
                cs = cs + c;
            }
        }
        str = content + cs;
        return str;
    }

    

    public static string MsgEncrypt(string msg)
    {
        string prefix = GetRandomString(1);
        char c = prefix.ToCharArray()[0];
        int len = c % 8;
        string s = GetRandomString(len);
        string key = PaddingLeft('@', 8, s);
        msg = EncodeDES(msg, key);
        return prefix + msg + s;
    }

    public static string MsgDecrypt(string msg)
    {
        char f = msg.ToCharArray()[0];
        int len = f % 8;
        string data = msg.Substring(1, msg.Length - 1 - len);
        string key = PaddingLeft('@', 8, msg.Substring(msg.Length - len, len));
        return DecodeDES(data, key);
    }
}