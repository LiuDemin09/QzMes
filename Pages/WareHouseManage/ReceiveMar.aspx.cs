using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;

public partial class Pages_WareHouseManage_ReceiveMar : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //註冊Ajax方法
        Utility.RegisterTypeForAjax(typeof(AjaxInterface));
    }
}