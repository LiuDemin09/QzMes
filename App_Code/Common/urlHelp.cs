using System;
using System.Web;
using System.Text;
namespace MES
{
    /// <summary>
    /// 地址栏操作类
    /// </summary>
    public static class urlHelp
    {
        /// <summary>
        /// 当前地址前缀
        /// </summary>
        public static string GetUrlPrefix
        {
            get
            {
                HttpRequest Request = HttpContext.Current.Request;
                string strUrl;
                strUrl = HttpContext.Current.Request.ServerVariables["Url"];
                if (HttpContext.Current.Request.QueryString.Count == 0) //如果无参数
                    return strUrl + "?page=";
                else
                {
                    if (HttpContext.Current.Request.ServerVariables["Query_String"].StartsWith("page=", StringComparison.OrdinalIgnoreCase))//只有页参数
                        return strUrl + "?page=";
                    else
                    {
                        string[] strUrl_left;
                        strUrl_left = HttpContext.Current.Request.ServerVariables["Query_String"].Split(new string[] { "page=" }, StringSplitOptions.None);
                        if (strUrl_left.Length == 1)//没有页参数
                            return strUrl + "?" + strUrl_left[0] + "&page=";
                        else
                            return strUrl + "?" + strUrl_left[0] + "page=";
                    }

                }
            }

        }
    }
}
