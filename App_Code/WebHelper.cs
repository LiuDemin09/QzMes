using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Freeworks.ORM;
using DAL;
using BLL;
/// <summary>
/// WebHelper 的摘要说明
/// </summary>
public class WebHelper
{
    public WebHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string GetAttachmentTempPath()
    {
        return HttpContext.Current.Server.MapPath("~/Uploads");
    }

    public static string GetAttachmentWebPath()
    {
        return "./Uploads";
    }

    public static string GetExtension(string fileName)
    {
        int startPos = fileName.LastIndexOf(".");
        string ext = fileName.Substring(startPos, fileName.Length - startPos);
        return ext;
    }

    public static bool ChkOnline()
    {
        return !(HttpContext.Current.Session[WebConstants.S_SESS_USER] == null);       
    }

    public static DataContext GetDB(UserInfo ui)
    {
        return DataServiceFactory.Create(ui.SiteCode + ui.BUCode);
    }

    public static UserInfo GetUserInfo()
    {
        UserInfo ui = (UserInfo)HttpContext.Current.Session[WebConstants.S_SESS_USER];
        if (ui == null)
        {
            ui = new UserInfo();
        }

        return ui;
    }

    public static string GetClientIP()
    {
        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
        else
            return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    }

    public static string GetClientIPv4Address()
    {
        string ipv4 = String.Empty;

        foreach (IPAddress ip in Dns.GetHostAddresses(GetClientIP()))
        {
            if (ip.AddressFamily.ToString() == "InterNetwork")
            {
                ipv4 = ip.ToString();
                break;
            }
        }

        if (ipv4 != String.Empty)
        {
            return ipv4;
        }
        // 利用 Dns.GetHostEntry 方法，由获取的 IPv6 位址反查 DNS 纪录，
        // 再逐一判断何者为 IPv4 协议，即可转为 IPv4 位址。
        foreach (IPAddress ip in Dns.GetHostEntry(GetClientIP()).AddressList)
        //foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (ip.AddressFamily.ToString() == "InterNetwork")
            {
                ipv4 = ip.ToString();
                break;
            }
        }

        return ipv4;
    }
}