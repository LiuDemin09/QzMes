using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using System.IO;
using System.Data;
using DAL;
using BLL;
/// <summary>
/// WsLogin 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 [System.Web.Script.Services.ScriptService]
public class WsLogin : System.Web.Services.WebService
{

    public WsLogin()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Login(string userCode, string pwd,bool isRemember)
    {
        try
        {
            UserInfo userInfo = new UserInfo();
            userInfo.UserCode = userCode;
           // userInfo.UserCode = userCode.ToUpper();
            userInfo.Password = pwd;
            userInfo.IP = WebHelper.GetClientIPv4Address();
            PubHelper.GetHelper(WebHelper.GetDB(userInfo)).DoLogin(userInfo);

            //userInfo.SiteName = PubHelper.GetHelper(WebHelper.GetDB(userInfo)).GetSiteName(userInfo.SiteCode);
            //userInfo.BUName = PubHelper.GetHelper(WebHelper.GetDB(userInfo)).GetBUName(userInfo.BUCode);
            //設置Session
            Session[WebConstants.S_SESS_USER] = userInfo;
            //寫入Cookie
            HttpCookie cookie = new HttpCookie("lang");
            cookie.Expires = DateTime.Now.AddDays(1);
            // cookie.Value = userInfo.Lang;
            HttpContext.Current.Response.Cookies.Add(cookie);

            //设置Cookies
            if (isRemember)
            {
                System.Collections.Specialized.NameValueCollection myCol = new System.Collections.Specialized.NameValueCollection();
                myCol.Add("name", userCode);
                myCol.Add("pwd", pwd);
                myCol.Add("ip", userInfo.IP);
                MES.Cookie.SetObj("mes_user", 60 * 60 * 15 * 1, myCol, "", "/");
            }
            else
            {
                MES.Cookie.Del("mes_user");
            }
            return "Main.aspx";


            // 记录其IP地址，下次登录时验证，IP为空则记录，IP不为空则验证
            //string uname = userCode;
            //string upwd = pwd;

            //if (uname.Equals("") || upwd.Equals(""))
            //{
            //    lblTip.Visible = true;
            //    lblTip.Text = "请输入用户名或密码";
            //    return;
            //}
            //Response.Redirect("index.aspx");


            //設置Session
            //Session["UserName"] = uname;

            //寫入Cookie
            //HttpCookie cookie = new HttpCookie("lang");
            //cookie.Expires = DateTime.Now.AddDays(1);
            //cookie.Value = userInfo.Lang;
            //HttpContext.Current.Response.Cookies.Add(cookie);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string getCookieValue()
    {
        string res = string.Empty;
        if (MES.Cookie.GetValue("mes_user") != null)
        {
            if (MES.Cookie.GetValue("mes_user", "ip") == WebHelper.GetClientIPv4Address())
            {
                res = MES.Cookie.GetValue("mes_user", "name");
                res += "," + MES.Cookie.GetValue("mes_user", "pwd");
            }
        }
        return res;
    }
}
