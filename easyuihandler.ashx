<%@ WebHandler Language="C#" Class="easyuihandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using BLL;
public class easyuihandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int pageIndex =Convert.ToInt32(context.Request.QueryString["page"]);  //当前页码  
        int pageSize = Convert.ToInt32(context.Request.QueryString["rows"]);  //每页显示记录数  
        string method = context.Request.QueryString["method"];//前台传的标示值   

        string JsonStr = string.Empty;
        try
        {
            switch (method)
            {
                //查询数据  
                case "query":
                    string dname =  context.Request.QueryString["dname"];//MSCL.RequestHelper.GetString("dname");  
                    string delse = context.Request.QueryString["delse"];// MSCL.RequestHelper.GetString("delse");  

                    string sort =  context.Request.QueryString["sort"];//MSCL.RequestHelper.GetString("sort"); //排序字段名。  
                    string order =  context.Request.QueryString["order"];//MSCL.RequestHelper.GetString("order"); //排序方式  
                    string where = string.Empty;
                    where += string.IsNullOrWhiteSpace(dname) ? "" : " And d_name like '%" + dname + "%' ";
                    where += string.IsNullOrWhiteSpace(delse) ? "" : " And d_else like '%" + delse + "%' ";
                    JsonStr = QueryData(pageIndex, pageSize, where, sort, order);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        context.Response.Write(JsonStr);
        context.Response.End();
    }

    #region   
    /// <summary>  
    /// 查询数据  
    /// </summary>  
    /// <param name="pageIndex">当前页码</param>  
    /// <param name="pageSize">每页记录数</param>  
    /// <param name="where">查询条件</param>  
    /// <param name="orderField">排序字段</param>  
    /// <param name="order">排序方式 asc或desc</param>  
    /// <returns></returns>  
    protected string QueryData(int pageIndex, int pageSize, string where,string orderField,string order)
    {
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
        return JsonConvert.SerializeObject(data);
    }
    #endregion  


    public bool IsReusable {
        get {
            return false;
        }
    }
    public class PageData
    {
        public int total;
        public List<JObject> rows;
    }
}