using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using Freeworks.Common;
using BLL;
using DAL;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Collections;
using System.Data;
using System.IO;
using Newtonsoft.Json;


/// <summary>
/// WsSystem 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WsOrder : WsBase
{
    private OrderBO _bal;
    public WsOrder() : base()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        _bal = BLLFactory.GetBal<OrderBO>(_userInfo);
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string FindMachineWOInfo()
    {
        IList<WorkOrder> objs = _bal.FindMachineWOInfo();

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        if (objs != null && objs.Count > 0)
        {
            foreach (WorkOrder o in objs)
            {
                sb.Append(string.Format("<row id='{0}'>", o.WO));
                sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
                sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
                sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
                sb.Append(string.Format("<cell>{0}</cell>", o.MaterialQty));
                sb.Append(string.Format("<cell>{0}</cell>", o.WO));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
                //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string FindOrderCreate()
    {
        DataSet ds = _bal.FindOrderCreate();


        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append(string.Format("<row id='{0}'>", ds.Tables[0].Rows[i]["ORDER_NO"] + "," + ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ORDER_NO"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CUST_NAME"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CUST_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["PLAN_QUANTITY"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["QUALITY_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ASK_QUANTITY"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["BATCH_NUMBER"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ASK_DATE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["UPDATED_BY"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CREATED_DATE"]));
                //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public object FindUnderPublishOrder(string orderNo, string partsdrawing, string startTime, string endTime)
    {
        DataSet ds = _bal.FindUnderPublishOrder(orderNo, partsdrawing, startTime, endTime);


        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append(string.Format("<row id='{0}'>", ds.Tables[0].Rows[i]["ORDER_NO"] + "," + ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
                // sb.Append(string.Format("<cell>< input name = \"Publish\" type = \"checkbox\" value =\"{0}\"></input></cell>", ds.Tables[0].Rows[i]["ORDER_NO"] + "," + ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
                sb.Append(string.Format("<cell></cell>"));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ORDER_NO"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CUST_NAME"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CUST_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["PLAN_QUANTITY"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["QUALITY_CODE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ASK_QUANTITY"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["BATCH_NUMBER"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ASK_DATE"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["UPDATED_BY"]));
                sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CREATED_DATE"]));
                //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string PublishOrder(string obj)
    {
        return _bal.PublishOrder(obj);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public object FindOrder(string orderNo, string partsdrawing, string startTime, string endTime)
    {
        IList<OrderDetail> objs = _bal.FindOrder(orderNo, partsdrawing, startTime, endTime);


        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        if (objs != null && objs.Count > 0)
        {
            foreach (OrderDetail o in objs)
            {
                sb.Append(string.Format("<row id='{0}'>", o.OrderNo + "," + o.PartsdrawingCode));

                sb.Append(string.Format("<cell>{0}</cell>", o.OrderNo));
                sb.Append(string.Format("<cell>{0}</cell>", o.CustName));
                sb.Append(string.Format("<cell>{0}</cell>", o.CONTRACT));
                sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
                sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
                sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.OutQuantity));
                sb.Append(string.Format("<cell>{0}</cell>", o.OutDate));
                sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
                sb.Append(string.Format("<cell>{0}</cell>", o.CreatedDate));
                //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public object FindWOOrder(string woNo, string partsdrawing, string startTime, string endTime)
    {
        IList<WorkOrder> objs = _bal.FindWOOrder(woNo, partsdrawing, startTime, endTime);


        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        if (objs != null && objs.Count > 0)
        {
            foreach (WorkOrder o in objs)
            {
                sb.Append(string.Format("<row id='{0}'>", o.WO));

                sb.Append(string.Format("<cell>{0}</cell>", o.WO));
                sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
                sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
                if (!string.IsNullOrEmpty(o.STATUS) & string.Equals(o.STATUS, "0"))
                {
                    sb.Append(string.Format("<cell>{0}</cell>", "创建"));
                }
                else if (!string.IsNullOrEmpty(o.STATUS) & string.Equals(o.STATUS, "1"))
                {
                    sb.Append(string.Format("<cell>{0}</cell>", "运行"));
                }
                else if (!string.IsNullOrEmpty(o.STATUS) & string.Equals(o.STATUS, "2"))
                {
                    sb.Append(string.Format("<cell>{0}</cell>", "暂停"));
                }
                else if (!string.IsNullOrEmpty(o.STATUS) & string.Equals(o.STATUS, "3"))
                {
                    sb.Append(string.Format("<cell>{0}</cell>", "关闭"));
                }
                else
                {
                    sb.Append(string.Format("<cell>{0}</cell>", o.STATUS));
                }
                sb.Append(string.Format("<cell>{0}</cell>", o.MachineType));
                sb.Append(string.Format("<cell>{0}</cell>", o.MachineName));
                sb.Append(string.Format("<cell>{0}</cell>", o.WorkerName));
                sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
                sb.Append(string.Format("<cell>{0}</cell>", o.StartTime));
                sb.Append(string.Format("<cell>{0}</cell>", o.EndTime));
                sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
                sb.Append(string.Format("<cell>{0}</cell>", o.PlanQuantity));
                //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
                sb.Append("</row>");
            }
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    //by tony add 2017-6-3
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FindUserNameByCode(string userCode)
    {
        WsSystem ws = new WsSystem();
        SysUser su = ws.FindUserByCode(userCode);
        //Dictionary<String, Object> map = new Dictionary<String, Object>();

        //    map.Add("name", su.UserName);

        //Context.Response.Write(JsonConvert.SerializeObject(map));
        if (su != null)
        {
            return su.UserName;
        }
        else
        {
            return "";
        }
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindMyOrder()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string orderNo = Context.Request.Form["orderNo"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string productname = Context.Request.Form["productname"];
        int count = _bal.FindMyOrderCount(orderNo, partsdrawing, productname);
        // DataSet ds = _bal.FindMyOrder(orderNo, partsdrawing,rows,page);
        IList<OrderDetail> ods = _bal.FindMyOrder(orderNo, partsdrawing, rows, page);
        //by tony modify 2017-6-3
        if (ods != null && ods.Count > 0)
        {
            foreach (OrderDetail od in ods)
            {

                od.CreatedBy = FindUserNameByCode(od.CreatedBy);
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        //if (count>0)
        //{
        map.Add("total", count);
        map.Add("rows", ods);
        //}
        string restemp = JsonConvert.SerializeObject(map);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DelOrderInfo(string orderNo, string contract)
    {
        return _bal.DelOrderInfo(orderNo, contract);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string DelOrderInfoByID(string ID)
    {
        return _bal.DelOrderInfo(ID);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindDrawing()
    {
        string partCode = Context.Request.Form["partCode"];
        string custCode = Context.Request.Form["custCode"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        IList<PartsdrawingCode> objs = _bal.FindDrawing(partCode, custCode, startTime, endTime);
        List<PartsdrawingCode> bs = new List<PartsdrawingCode>();
        int istart = (Convert.ToInt32(page) - 1) * Convert.ToInt32(rows);
        int iend = Convert.ToInt32(page) * Convert.ToInt32(rows) + 1;
        int j = 1;
        foreach (PartsdrawingCode bb in objs)
        {
            if (j > istart && j < iend)
            {
                PartsdrawingCode bbtemp = new PartsdrawingCode();
                bbtemp = bb;
                bbtemp.UpdatedBy = FindUserNameByCode(bbtemp.UpdatedBy);
                bs.Add(bbtemp);
            }
            j++;
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();

        //if (objs != null & objs.Count > 0)
        //{
        map.Add("total", objs.Count);
        map.Add("rows", objs);
        //}
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindAllOrder()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string orderNo = Context.Request.Form["orderNo"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string OrderStatus = Context.Request.Form["OrderStatus"];//by tony add 2017-6-6
        string contract = Context.Request.Form["contract"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        int count = _bal.FindAllOrderCount(orderNo, partsdrawing, startTime, endTime, OrderStatus, contract);
        DataSet ds = _bal.FindAllOrder(orderNo, partsdrawing, startTime, endTime, rows, page, OrderStatus, contract);
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CREATED_BY"] != null)
                {
                    ds.Tables[0].Rows[i]["CREATED_BY"] = FindUserNameByCode(ds.Tables[0].Rows[i]["CREATED_BY"].ToString());
                }
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();

        //if (count>0)
        //{

        map.Add("total", count);
        map.Add("rows", ds.Tables[0]);


        //}
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindAllPublishOrder()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string orderNo = Context.Request.Form["orderNo"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        int count = _bal.FindAllPublishOrderCount(orderNo, partsdrawing, startTime, endTime);
        DataSet ds = _bal.FindAllPublishOrder(orderNo, partsdrawing, startTime, endTime, rows, page);
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CREATED_BY"] != null)
                {

                    ds.Tables[0].Rows[i]["CREATED_BY"] = FindUserNameByCode(ds.Tables[0].Rows[i]["CREATED_BY"].ToString());
                }
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        //if (count > 0)
        //{

        map.Add("total", count);
        map.Add("rows", ds.Tables[0]);


        //}
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindAllSendOrder()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string orderNo = Context.Request.Form["orderNo"];
        string partsdrawing = Context.Request.Form["partsdrawing"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        int count = _bal.FindAllSendOrderCount(orderNo, partsdrawing);
        DataSet ds = _bal.FindAllSendOrder(orderNo, partsdrawing, rows, page);
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                object ask = (ds.Tables[0].Rows[i]["ASK_QUANTITY"] == null || ds.Tables[0].Rows[i]["ASK_QUANTITY"].ToString() == "") ? 0 : ds.Tables[0].Rows[i]["ASK_QUANTITY"];
                object outQty = (ds.Tables[0].Rows[i]["OUT_QUANTITY"] == null || ds.Tables[0].Rows[i]["OUT_QUANTITY"].ToString() == "") ? 0 : ds.Tables[0].Rows[i]["OUT_QUANTITY"];
                ds.Tables[0].Rows[i]["ASK_QUANTITY"] = ask;
                ds.Tables[0].Rows[i]["OUT_QUANTITY"] = outQty;
                ds.Tables[0].Rows[i]["OUT_NOTICE_QTY"] = Convert.ToInt32(ask) - Convert.ToInt32(outQty);
                if (ds.Tables[0].Rows[i]["CREATED_BY"] != null)
                {

                    ds.Tables[0].Rows[i]["CREATED_BY"] = FindUserNameByCode(ds.Tables[0].Rows[i]["CREATED_BY"].ToString());
                }
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        //if (count > 0)
        //{

        map.Add("total", count);
        map.Add("rows", ds.Tables[0]);


        //}
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SendOrder(string obj)
    {
        return _bal.SendOrder(obj);
    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string RejectOrder(string obj)
    {
        return _bal.RejectOrder(obj);
    }
}
