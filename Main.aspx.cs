using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
//using AjaxPro;
public partial class Main : System.Web.UI.Page
{
    protected UserInfo userInfo = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        userInfo = WebHelper.GetUserInfo();
        //註冊Ajax方法
       // Utility.RegisterTypeForAjax(typeof(AjaxInterface));
    }
}