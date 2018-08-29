using System;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;
namespace MES
{
    /// <summary>
    /// 一些常用的字符串函数
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// 倒序加1加密
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static string EncryptStr(string rs) //倒序加1加密 
        {
            byte[] by = new byte[rs.Length];
            for (int i = 0; i <= rs.Length - 1; i++)
            {
                by[i] = (byte)((byte)rs[i] + 1);
            }
            rs = "";
            for (int i = by.Length - 1; i >= 0; i--)
            {
                rs += ((char)by[i]).ToString();
            }
            return rs;
        }
        /// <summary>
        /// 顺序减1解码 
        /// </summary>
        /// <param name="rs"></param>
        /// <returns></returns>
        public static string DecryptStr(string rs) //顺序减1解码 
        {
            byte[] by = new byte[rs.Length];
            for (int i = 0; i <= rs.Length - 1; i++)
            {
                by[i] = (byte)((byte)rs[i] - 1);
            }
            rs = "";
            for (int i = by.Length - 1; i >= 0; i--)
            {
                rs += ((char)by[i]).ToString();
            }
            return rs;
        }
        /// <summary>
        /// Escape加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Escape(string str)
        {
            if (str == null)
                return String.Empty;
            StringBuilder sb = new StringBuilder();
            int len = str.Length;

            for (int i = 0; i < len; i++)
            {
                char c = str[i];

                //everything other than the optionally escaped chars _must_ be escaped 
                if (Char.IsLetterOrDigit(c) || c == '-' || c == '_' || c == '/' || c == '\\' || c == '.')
                    sb.Append(c);
                else
                    sb.Append(Uri.HexEscape(c));
            }

            return sb.ToString();
        }
        /// <summary>
        /// UnEscape解密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnEscape(string str)
        {
            if (str == null)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            int len = str.Length;
            int i = 0;
            while (i != len)
            {
                if (Uri.IsHexEncoding(str, i))
                    sb.Append(Uri.HexUnescape(str, ref i));
                else
                    sb.Append(str[i++]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 左截取
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(string inputString, int len)
        {
            if (inputString.Length < len)
                return inputString;
            else
                return inputString.Substring(0, len)+"…";
        }
        /// <summary>
        /// 右截取
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(string inputString, int len)
        {
            if (inputString.Length < len)
                return inputString;
            else
                return inputString.Substring(inputString.Length - len, len);
        }
        /// <summary>
        /// 截取指定长度字符串,汉字为2个字符
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (tempLen > len)
                    break;
            }
            return tempString;
        }
        #region 去除HTML标记
        ///<summary>   
        ///去除HTML标记   
        ///</summary>   
        ///<param name="NoHTML">包括HTML的源码</param>   
        ///<returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            //Regex myReg = new Regex(@"(\<.[^\<]*\>)", RegexOptions.IgnoreCase);
            //Htmlstring = myReg.Replace(Htmlstring, "");
            //myReg = new Regex(@"(\<\/[^\<]*\>)", RegexOptions.IgnoreCase);
            //Htmlstring = myReg.Replace(Htmlstring, "");
            //return Htmlstring;

            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&ldquo;", "“", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&rdquo;", "”", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring = Htmlstring.Replace("<", "&lt;");
            Htmlstring = Htmlstring.Replace(">", "&gt;");
            return Htmlstring;
        }
        #endregion
        /// <summary>
        /// 不区分大小写的替换
        /// </summary>
        /// <param name="original">原字符串</param>
        /// <param name="pattern">需替换字符</param>
        /// <param name="replacement">被替换内容</param>
        /// <returns></returns>
        public static string ReplaceEx(string original, string pattern, string replacement)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = original.ToUpper();
            string upperPattern = pattern.ToUpper();
            int inc = (original.Length / pattern.Length) * (replacement.Length - pattern.Length);
            char[] chars = new char[original.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern, position0)) != -1)
            {
                for (int i = position0; i < position1; ++i) chars[count++] = original[i];
                for (int i = 0; i < replacement.Length; ++i) chars[count++] = replacement[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return original;
            for (int i = position0; i < original.Length; ++i) chars[count++] = original[i];
            return new string(chars, 0, count);
        }
        /// <summary>
        /// 替换html中的特殊字符
        /// </summary>
        /// <param name="theString">需要进行替换的文本。</param>
        /// <returns>替换完的文本。</returns>
        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("'", "&#39;");
            theString = theString.Replace("\r\n", "<br/> ");
            return theString;
        }

        /// <summary>
        /// 恢复html中的特殊字符
        /// </summary>
        /// <param name="theString">需要恢复的文本。</param>
        /// <returns>恢复好的文本。</returns>
        public static string HtmlDecode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("<br/> ", "\r\n");
            return theString;
        }
        /// <summary>
        /// 输出单行简介
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string SimpleLineSummary(string theString)
        {
            theString = theString.Replace("&gt;", "");
            theString = theString.Replace("&lt;", "");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "'");
            theString = theString.Replace("<br/> ", "\r\n");
            theString = theString.Replace("\"", "");
            theString = theString.Replace("\t", " ");
            theString = theString.Replace("\r", " ");
            theString = theString.Replace("\n", " ");
            theString = Regex.Replace(theString, "\\s{2,}", " ");
            return theString;
        }
        /// <summary> 
        /// UBB代码处理函数 
        /// </summary> 
        /// <param name="content">输入字符串</param> 
        /// <returns>输出字符串</returns> 
        public static string UBB2HTML(string content)  //ubb转html
        {
            content = Regex.Replace(content, @"\[b\](.+?)\[/b\]", "<b>$1</b>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[i\](.+?)\[/i\]", "<i>$1</i>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[u\](.+?)\[/u\]", "<u>$1</u>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[p\](.+?)\[/p\]", "<p>$1</p>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[align=left\](.+?)\[/align\]", "<align='left'>$1</align>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[align=center\](.+?)\[/align\]", "<align='center'>$1</align>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[align=right\](.+?)\[/align\]", "<align='right'>$1</align>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[url=(?<url>.+?)]\[/url]", "<a href='${url}' target=_blank>${url}</a>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[url=(?<url>.+?)](?<name>.+?)\[/url]", "<a href='${url}' target=_blank>${name}</a>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[quote](?<text>.+?)\[/quote]", "<div class=\"quote\">${text}</div>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"\[img](?<img>.+?)\[/img]", "<a href='${img}' target=_blank><img src='${img}' alt=''/></a>", RegexOptions.IgnoreCase);
            return content;
        }
        /// <summary>
        /// 将html转成js代码,不完全和原始数据一致
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Html2Js(string source)
        {
            return String.Format("document.write(\"{0}\");",
                String.Join("\");\r\ndocument.write(\"", source.Replace("\\", "\\\\")
                                        .Replace("/", "\\/")
                                        .Replace("'", "\\'")
                                        .Replace("\"", "\\\"")
                                        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            ));
        }
        /// <summary>
        /// 将html转成可输出的js字符串,不完全和原始数据一致
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Html2JsStr(string source)
        {
            return String.Format("{0}",
                String.Join(" ", source.Replace("\\", "\\\\")
                                        .Replace("/", "\\/")
                                        .Replace("'", "\\'")
                                        .Replace("\"", "\\\"")
                                        .Replace("\t", "")
                                        .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                            ));
        }
        /// <summary>
        /// 过滤所有特殊特号
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string FilterSymbol(string theString)
        {
            string[] aryReg = { "'", "\"", "\r", "\n", "<", ">", "%", "?", ",", ".", "=", "-", "_", ";", "|", "[", "]", "&", "/" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        /// <summary>
        /// 过滤一般特殊特号,如单引、双引和回车和分号等
        /// </summary>
        /// <param name="theString"></param>
        /// <returns></returns>
        public static string SafetyStr(string theString)
        {
            string[] aryReg = { "'", ";", "\"", "\r", "\n" };
            for (int i = 0; i < aryReg.Length; i++)
            {
                theString = theString.Replace(aryReg[i], string.Empty);
            }
            return theString;
        }
        /// <summary>
        /// 正则表达式取值
        /// </summary>
        /// <param name="HtmlCode">HTML代码</param>
        /// <param name="RegexString">正则表达式</param>
        /// <param name="GroupKey">正则表达式分组关键字</param>
        /// <param name="RightToLeft">是否从右到左</param>
        /// <returns></returns>
        public static string[] GetRegValue(string HtmlCode, string RegexString, string GroupKey, bool RightToLeft)
        {
            MatchCollection m;
            Regex r;
            if (RightToLeft == true)
            {
                r = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.RightToLeft);
            }
            else
            {
                r = new Regex(RegexString, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            }
            m = r.Matches(HtmlCode);
            string[] MatchValue = new string[m.Count];
            for (int i = 0; i < m.Count; i++)
            {
                MatchValue[i] = m[i].Groups[GroupKey].Value;
            }
            return MatchValue;
        }
        /// <summary>
        /// 获得标签的属性值
        /// </summary>
        /// <param name="HtmlTag"></param>
        /// <param name="AttributeName"></param>
        /// <returns></returns>
        public static string AttributeValue(string HtmlTag, string AttributeName)
        {
            string RegexString = AttributeName + "=(\"|')(?<" + AttributeName + ">.*?)(\\1)";
            string[] _att = MES.Strings.GetRegValue(HtmlTag, RegexString, AttributeName, false);
            if (_att.Length > 0)
                return _att[0].ToString();
            else
                return "";
        }
        public static string delhtml(string Htmlstring)
        {
            Regex myReg = new Regex(@"(\<.[^\<]*\>)", RegexOptions.IgnoreCase);
            Htmlstring = myReg.Replace(Htmlstring, "");
            myReg = new Regex(@"(\<\/[^\<]*\>)", RegexOptions.IgnoreCase);
            Htmlstring = myReg.Replace(Htmlstring, "");
            return Htmlstring;
        }
    }
}
