using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BLL;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage:Page
{
    public UserInfo userInfo = null;

	public BasePage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected override void InitializeCulture()
    {
        if (!WebHelper.ChkOnline())
        {
           // Response.Redirect("../../Login.aspx");
            Response.Write("<script languge='javascript'>var top=window;while(top!=top.parent){top=top.parent;}top.location.href='../../Login.aspx'</script>");
        }
        userInfo = WebHelper.GetUserInfo();
        this.UICulture = this.Culture = this.userInfo.Lang;
        base.InitializeCulture();
    }

    protected override void OnLoad(EventArgs e)
    {
        
        base.OnLoad(e);
    }
}