using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Freeworks.Common;
using BLL;
/// <summary>
/// WsBase 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
 //[System.Web.Script.Services.ScriptService]
public class WsBase : System.Web.Services.WebService
{
    protected UserInfo _userInfo;
    public WsBase()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        //if (!WebHelper.ChkOnline())
        //{
        //    Thread.CurrentThread.CurrentUICulture =
        //    CultureInfo.CreateSpecificCulture(HttpContext.Current.Request.Cookies.Get("lang").Value);
        //    throw new BusiException(Resources.MsgRes.M0001, "E0001");
        //}

        _userInfo = WebHelper.GetUserInfo();
        //Set server-side script timeout
        this.Context.Server.ScriptTimeout = 1 * 60 * 100;
        string path = Context.Request.Url.ToString();
        int sIdx = path.LastIndexOf('/') + 1;
        string methodName = path.Substring(sIdx, path.Length - sIdx);
    }
 
}
