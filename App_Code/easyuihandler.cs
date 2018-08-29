using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using BLL;
using System.Web.Script.Services;
/// <summary>
/// easyuihandler 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class easyuihandler : System.Web.Services.WebService
{

    public easyuihandler()
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
    public void QueryData()
    {
        int pageIndex = 0;
        int pageSize = 0;
        string where = "";
        string orderField = null;
        string order = null;
        int totalRecord = 0;
        int TotalPage = 0;
        string orderStr = string.IsNullOrWhiteSpace(orderField) ? "d_id asc" : string.Format("{0} {1}", orderField, order);
        UserInfo ui = new UserInfo();
        ui.SiteCode = "Mes";
        ui.BUCode = "LF";
        WareHouseBO wbo = new WareHouseBO(ui);
        DataSet ds = wbo.FindStandByOutWH();// MSCL.PagingHelper.QueryPagingMssql("TestTable", "*", orderStr, pageIndex, pageSize, where, out totalRecord, out TotalPage);
        DataTable dt = ds.Tables[0];
        PageData data = new PageData();
        data.total = totalRecord;
        List<JObject> list = new List<JObject>();
        foreach (DataRow item in dt.Rows)
        {
            JObject obj = new JObject();
            obj.Add("d_id", item["ORDER_NO"].ToString());
            obj.Add("d_name", item["WO"].ToString());
            obj.Add("d_password", item["PARTSDRAWING_CODE"].ToString());
            obj.Add("d_else", item["STATUS"].ToString());
            obj.Add("d_amount", item["PRODUCT_NAME"].ToString());
            list.Add(obj);
        }
        data.rows = list;
        Context.Response.Write(JsonConvert.SerializeObject(data));
        //return JsonConvert.SerializeObject(data);
    }
}
public class PageData
{
    public int total;
    public List<JObject> rows;
}