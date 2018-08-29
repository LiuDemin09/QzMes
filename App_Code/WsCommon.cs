using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DAL;
using BLL;
using System.Web.Script.Services;
/// <summary>
/// WsCommon 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class WsCommon : System.Web.Services.WebService
{

    public WsCommon()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    /// <summary>
    /// 保持帳號在綫
    /// </summary>
    /// <param name="ui"></param>
    /// <returns></returns>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public bool KeepOnline(UserInfo ui)
    {
        if (!WebHelper.ChkOnline())
        {
            Session[WebConstants.S_SESS_USER] = ui;
        }

        return true;
    }

    //[WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public string Login(string userCode, string pwd, string lang, string loc)
    //{
    //    try
    //    {

    //        UserInfo userInfo = new UserInfo();
    //        //userInfo.UserCode = userCode;
    //        userInfo.UserCode = userCode.ToUpper(); //zhiwei20141015
    //        userInfo.Password = pwd;
    //        userInfo.Lang = lang;
    //        userInfo.IP = WebHelper.GetClientIPv4Address();
    //        userInfo.SiteCode = loc.Split('^')[0];
    //        userInfo.BUCode = loc.Split('^')[1];
    //        PubHelper.GetHelper(WebHelper.GetDB(userInfo)).DoLogin4Web(userInfo);

    //        userInfo.SiteName = PubHelper.GetHelper(WebHelper.GetDB(userInfo)).GetSiteName(userInfo.SiteCode);
    //        userInfo.BUName = PubHelper.GetHelper(WebHelper.GetDB(userInfo)).GetBUName(userInfo.BUCode);
    //        //設置Session
    //        Session[WebConstants.S_SESS_USER] = userInfo;
    //        //寫入Cookie
    //        HttpCookie cookie = new HttpCookie("lang");
    //        cookie.Expires = DateTime.Now.AddDays(1);
    //        cookie.Value = userInfo.Lang;
    //        HttpContext.Current.Response.Cookies.Add(cookie);

    //        string iiiid = PubHelper.GetHelper(WebHelper.GetDB(userInfo)).GetNextID().ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        log4net.ILog log = log4net.LogManager.GetLogger("WsCommon");
    //        log.Error(ex);

    //        throw ex;
    //    }
    //    return "./MDSystem/Index.aspx";
    //}

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public SPMessage GetLabelParameters(string data, string labelId)
    {
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(WebHelper.GetUserInfo());
        return _bal.GetLabelParameters(data, labelId);
    }

     


    //[WebMethod(true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //public SPMessage GetUserLabelParameters(string data, string labelId)
    //{
    //    RouteBO _bal = BLLFactory.GetBal<RouteBO>(WebHelper.GetUserInfo());
    //    return _bal.GetUserLabelParameters(data, labelId);
    //}

}
