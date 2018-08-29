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
using Newtonsoft.Json;
using Freeworks.Common.Paging;
/// <summary>
/// WsSystem 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class WsWareHouse : WsBase
{
    private WareHouseBO _bal;
    public WsWareHouse() : base()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        _bal = BLLFactory.GetBal<WareHouseBO>(_userInfo);
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveProductInWH(string transferEmp, string delivery, string productCode)
    {
        //string[] psns = psn.Split(',');
        //for(int i=0;i<psns.Length;i++)
        //{
        //    string temppsn = psns[i];
        //    temppsn = temppsn.Replace("\n", "");
        //    temppsn = temppsn.Replace("\r", "");
        //    _bal.SaveProductInWH("086", "QZB1704000302", temppsn);
        //}
        //return "OK";
        return _bal.SaveProductInWH(transferEmp, delivery, productCode);
    }




    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListTopProductInWH()
    {
        IList<ProductStock> objs = _bal.ListTopProductInWH(BLLConstants.I_SHOW_NUM);
        if (objs != null && objs.Count > 0)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                //objs[i].StockHouse = _bal.FindNameBySubCode(objs[i].StockHouse) == null ? objs[i].StockHouse : _bal.FindNameBySubCode(objs[i].StockHouse).SubName;
                //objs[i].MANUFACTURE = _bal.FindCustNameByCode(objs[i].MANUFACTURE) == null ? objs[i].MANUFACTURE : _bal.FindCustNameByCode(objs[i].MANUFACTURE).NAME;
                objs[i].FromBy = _bal.FindUserNameByCode(objs[i].UpdatedBy) == null ? objs[i].UpdatedBy : _bal.FindUserNameByCode(objs[i].UpdatedBy).UserName;
                objs[i].UpdatedBy = _bal.FindUserNameByCode(objs[i].UpdatedBy) == null ? objs[i].UpdatedBy : _bal.FindUserNameByCode(objs[i].UpdatedBy).UserName;
            }
        }

        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //if (objs != null && objs.Count > 0)
        //{
        //    foreach (ProductStock o in objs)
        //    {
        //        sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.MANUFACTURE));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.StockHouse));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductCode));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.UNIT));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.FromBy));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate));
        //        //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //        sb.Append("</row>");
        //    }
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", BLLConstants.I_SHOW_NUM);
        map.Add("rows", objs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindStandByOutWH()
    {
        DataSet ds = _bal.FindStandByOutWH();
        if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["STATUS"] != null && ds.Tables[0].Rows[i]["STATUS"].ToString() == "0")
                {
                    ds.Tables[0].Rows[i]["STATUS"] = "创建";
                }
                else if (ds.Tables[0].Rows[i]["STATUS"] != null && ds.Tables[0].Rows[i]["STATUS"].ToString() == "1")
                {
                    ds.Tables[0].Rows[i]["STATUS"] = "运行";
                }
                else if (ds.Tables[0].Rows[i]["STATUS"] != null && ds.Tables[0].Rows[i]["STATUS"].ToString() == "2")
                {
                    ds.Tables[0].Rows[i]["STATUS"] = "暂停";
                }
                else if (ds.Tables[0].Rows[i]["STATUS"] != null && ds.Tables[0].Rows[i]["STATUS"].ToString() == "3")
                {
                    ds.Tables[0].Rows[i]["STATUS"] = "关闭";
                }

            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", ds.Tables.Count);
        map.Add("rows", ds.Tables[0]);
        Context.Response.Write(JsonConvert.SerializeObject(map));

        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //{

        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        sb.Append(string.Format("<row id='{0}'>", ds.Tables[0].Rows[i]["ORDER_NO"] + "," + ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ORDER_NO"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["WO"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["PARTSDRAWING_CODE"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["STATUS"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["PRODUCT_NAME"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CREATED_DATE"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["UPDATED_DATE"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["BATCH_NUMBER"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["ORDER_QUANTITY"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["CHECK_TIME"]));
        //        sb.Append(string.Format("<cell>{0}</cell>", ds.Tables[0].Rows[i]["IN_TIME"]));
        //        //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //        sb.Append("</row>");
        //    }
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindStandByDetail(string workOrder)
    {
        IList<ProductStock> objs = _bal.FindStandByDetail(workOrder);
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", objs.Count);
        map.Add("rows", objs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //if (objs != null && objs.Count > 0)
        //{
        //    foreach (ProductStock o in objs)
        //    {
        //        sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.MANUFACTURE));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.StockHouse));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductCode));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //        //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //        sb.Append("</row>");
        //    }
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string SaveProductOutWH(string productCode)
    {
        return _bal.SaveProductOutWH(productCode);
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindProductOutWH()
    {
        IList<ProductStock> objs = _bal.FindProductOutWH();
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", objs.Count);
        map.Add("rows", objs);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //if (objs != null && objs.Count > 0)
        //{
        //    foreach (ProductStock o in objs)
        //    {
        //        sb.Append(string.Format("<row id='{0}'>", o.PSN));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.MANUFACTURE));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.StockHouse));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductCode));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.UNIT));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //        sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate));
        //        //sb.Append(string.Format("<cell>View^javascript:MySite.Runner.showDetail({0})^_self</cell>", log.ID));
        //        sb.Append("</row>");
        //    }
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryProductStock(string workorder, string productname, string batchnumber, string starttime, string endtime)
    {
        IList<ProductStock> objs = _bal.FindProductStockInfo(workorder, productname, batchnumber, starttime, endtime);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (ProductStock o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.PSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
            if (o.MANUFACTURE.Length == 13)
            {
                BasBase ibbb = _bal.FindNameBySubCode(o.MANUFACTURE);
                sb.Append(string.Format("<cell>{0}</cell>", ibbb.SubName));
            }
            else
            {
                sb.Append(string.Format("<cell>{0}</cell>", o.MANUFACTURE));
            }
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            BasBase ibbbb = _bal.FindNameBySubCode(o.StockHouse);
            sb.Append(string.Format("<cell>{0}</cell>", ibbbb.SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            BasBase ibb = _bal.FindNameBySubCode(o.UNIT);
            sb.Append(string.Format("<cell>{0}</cell>", ibb.SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY.ToString()));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.FromBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            //sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string QueryProductINOrOut(string workorder, string status, string productname, string batchnumber, string starttime, string endtime)
    {
        if (status == "3")
        {
            status = "";
        }
        IList<ProductStockHistory> objs = _bal.FindProductStockHistory(status, workorder, productname, batchnumber, starttime, endtime);

        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<rows>");
        string strstatus = string.Empty;
        foreach (ProductStockHistory o in objs)
        {
            sb.Append(string.Format("<row id='{0}'>", o.ID));
            sb.Append(string.Format("<cell>{0}</cell>", o.PSN));
            sb.Append(string.Format("<cell>{0}</cell>", o.WorkOrder));
            if (o.MANUFACTURE.Length == 13)
            {
                BasBase ibbb = _bal.FindNameBySubCode(o.MANUFACTURE);
                sb.Append(string.Format("<cell>{0}</cell>", ibbb.SubName));
            }
            else
            {
                sb.Append(string.Format("<cell>{0}</cell>", o.MANUFACTURE));
            }
            sb.Append(string.Format("<cell>{0}</cell>", o.DOCUMENTID));
            BasBase ibbbb = _bal.FindNameBySubCode(o.StockHouse);
            sb.Append(string.Format("<cell>{0}</cell>", ibbbb.SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductCode));
            sb.Append(string.Format("<cell>{0}</cell>", o.ProductName));
            BasBase ibb = _bal.FindNameBySubCode(o.UNIT);
            sb.Append(string.Format("<cell>{0}</cell>", ibb.SubName));
            sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY.ToString()));
            sb.Append(string.Format("<cell>{0}</cell>", o.BatchNumber));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.FromBy));
            sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
            //sb.Append(string.Format("<cell>{0}</cell>", o.MEMO));
            sb.Append("</row>");
        }
        sb.Append("</rows>");
        return sb.ToString();
    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryProductINOut()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string workorder = Context.Request.Form["workOrder"];
        string productname = Context.Request.Form["productName"];
        string batchnumber = Context.Request.Form["batch"];
        string status = Context.Request.Form["status"];
        string starttime = Context.Request.Form["startTime"];
        string endtime = Context.Request.Form["endTime"];
        if (status == "4")
        {
            status = "";
        }
        PagingResult<ProductStockHistory> objs = _bal.FindProductStockHistory(status, workorder, productname, batchnumber, starttime, endtime, rows, page);
        if (objs != null && objs.ResultSet.Count > 0)
        {
            for (int i = 0; i < objs.ResultSet.Count; i++)
            {
                objs.ResultSet[i].StockHouse = _bal.FindNameBySubCode(objs.ResultSet[i].StockHouse) == null ? objs.ResultSet[i].StockHouse : _bal.FindNameBySubCode(objs.ResultSet[i].StockHouse).SubName;
                objs.ResultSet[i].MANUFACTURE = _bal.FindNameBySubCode(objs.ResultSet[i].MANUFACTURE) == null ? objs.ResultSet[i].MANUFACTURE : _bal.FindNameBySubCode(objs.ResultSet[i].MANUFACTURE).SubName;
                objs.ResultSet[i].UNIT = _bal.FindNameBySubCode(objs.ResultSet[i].UNIT) == null ? objs.ResultSet[i].UNIT : _bal.FindNameBySubCode(objs.ResultSet[i].UNIT).SubName;
                objs.ResultSet[i].UpdatedBy = _bal.FindUserNameByCode(objs.ResultSet[i].UpdatedBy) == null ? objs.ResultSet[i].UpdatedBy : _bal.FindUserNameByCode(objs.ResultSet[i].UpdatedBy).UserName;
                objs.ResultSet[i].FromBy = _bal.FindUserNameByCode(objs.ResultSet[i].FromBy) == null ? objs.ResultSet[i].FromBy : _bal.FindUserNameByCode(objs.ResultSet[i].FromBy).UserName;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", objs.ResultCount);
        map.Add("rows", objs.ResultSet);
        Context.Response.Write(JsonConvert.SerializeObject(map));


    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindPrepareMar()
    {
        string workorder = Context.Request.Form["workorder"];
        string partsdrawingno = Context.Request.Form["partsdrawingno"];
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        int count = _bal.FindOnlineWOCount();
        IList<WorkOrder> wo = _bal.FindOnlineWO(rows, page, workorder, partsdrawingno);
        if (wo != null && wo.Count > 0)
        {
            for (int i = 0; i < wo.Count; i++)
            {

                IList<MaterialStock> objs = _bal.QueryPrepareInfo(wo[i].WO);
                wo[i].MEMO = _bal.FindQMaterialByCustMaterial(wo[i].PartsdrawingCode);//钦纵料号 by tony add 2017-6-19
                if (objs != null && objs.Count > 0)
                {
                    BasCustom ibb = _bal.FindCustNameByCode(objs[0].CustName);
                    IList<BasBase> ibb1 = _bal.FindBaseBySubCode(objs[0].StockHouse);
                    
                    if (ibb != null && !string.IsNullOrEmpty(ibb.NAME))
                    {
                        wo[i].CustName = ibb.NAME;
                    }
                    else
                    {

                        wo[i].CustName = objs[0].CustName;
                    }
                    if (ibb1 != null && ibb1.Count > 0)
                    {
                        wo[i].MachineName = ibb1[0].SubName;
                    }
                    else
                    {

                        wo[i].MachineName = objs[0].StockHouse;
                    }
                    wo[i].QUANTITY = objs.Count;
                   
                }
                else
                {
                    wo[i].MachineName = "";
                    wo[i].QUANTITY = 0;
                }
            }

        }

        Dictionary<String, Object> map = new Dictionary<String, Object>();


        map.Add("total", count);
        map.Add("rows", wo);
        Context.Response.Write(JsonConvert.SerializeObject(map));



    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindOutWH()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        int count = _bal.FindOrderDetailCount("2");
        IList<OrderDetail> ods = _bal.FindOrderDetail(rows, page, "2");
        if (ods != null & ods.Count > 0)
        {
            foreach (OrderDetail od in ods)
            {
                int inWH = od.InQuantity == null ? 0 : Convert.ToInt32(od.InQuantity);
                int outWH = od.OutQuantity == null ? 0 : Convert.ToInt32(od.OutQuantity);
                od.InQuantity = inWH;
                od.OutQuantity = outWH;
                int stock = inWH - outWH;
                od.MEMO = stock.ToString();

            }
        }

        Dictionary<String, Object> map = new Dictionary<String, Object>();


        map.Add("total", count);
        map.Add("rows", ods);
        Context.Response.Write(JsonConvert.SerializeObject(map));



    }


    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindReceiveMar()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string materialCode = Context.Request.Form["materialCode"];
        int count = _bal.FindReceiveMarCount(materialCode);
        IList<MaterialStock> ms = _bal.FindReceiveMar(rows, page, materialCode);
        if (ms != null && ms.Count > 0)
        {
            for (int i = 0; i < ms.Count; i++)
            {
                ms[i].StockHouse = _bal.FindNameBySubCode(ms[i].StockHouse) == null ? ms[i].StockHouse : _bal.FindNameBySubCode(ms[i].StockHouse).SubName;
                ms[i].CustName = _bal.FindCustNameByCode(ms[i].CustName) == null ? ms[i].CustName : _bal.FindCustNameByCode(ms[i].CustName).NAME;
                ms[i].UpdatedBy = _bal.FindUserNameByCode(ms[i].UpdatedBy) == null ? ms[i].UpdatedBy : _bal.FindUserNameByCode(ms[i].UpdatedBy).UserName;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", count);
        map.Add("rows", ms);
        Context.Response.Write(JsonConvert.SerializeObject(map));



    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindPreparedMar()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string workOrder = Context.Request.Form["workOrder"];
        int count = _bal.FindPreparedMarCount(workOrder);
        IList<MaterialStock> ms = _bal.FindPreparedMar(rows, page, workOrder);
        if (ms != null && ms.Count > 0)
        {
            for (int i = 0; i < ms.Count; i++)
            {
                ms[i].StockHouse = _bal.FindNameBySubCode(ms[i].StockHouse) == null ? ms[i].StockHouse : _bal.FindNameBySubCode(ms[i].StockHouse).SubName;
                ms[i].CustName = _bal.FindCustNameByCode(ms[i].CustName) == null ? ms[i].CustName : _bal.FindCustNameByCode(ms[i].CustName).NAME;
                ms[i].UpdatedBy = _bal.FindUserNameByCode(ms[i].UpdatedBy) == null ? ms[i].UpdatedBy : _bal.FindUserNameByCode(ms[i].UpdatedBy).UserName;
                ms[i].UNIT = _bal.FindNameBySubCode(ms[i].UNIT) == null ? ms[i].UNIT : _bal.FindNameBySubCode(ms[i].UNIT).SubName;
            }
        }

        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", count);
        map.Add("rows", ms);
        Context.Response.Write(JsonConvert.SerializeObject(map));



    }



    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindSendMar()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string workOrder = Context.Request.Form["workOrder"];
        int count = _bal.FindSendMarCount(workOrder, "1");
        IList<MaterialStockHistory> ms = _bal.FindSendMar(rows, page, workOrder, "1");
        if (ms != null && ms.Count > 0)
        {
            for (int i = 0; i < ms.Count; i++)
            {
                ms[i].StockHouse = _bal.FindNameBySubCode(ms[i].StockHouse) == null ? ms[i].StockHouse : _bal.FindNameBySubCode(ms[i].StockHouse).SubName;
                ms[i].CustName = _bal.FindCustNameByCode(ms[i].CustName) == null ? ms[i].CustName : _bal.FindCustNameByCode(ms[i].CustName).NAME;
                ms[i].UpdatedBy = _bal.FindUserNameByCode(ms[i].UpdatedBy) == null ? ms[i].UpdatedBy : _bal.FindUserNameByCode(ms[i].UpdatedBy).UserName;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", count);
        map.Add("rows", ms);
        Context.Response.Write(JsonConvert.SerializeObject(map));



    }



    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void FindMarInOut()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string workOrder = Context.Request.Form["workOrder"];
        int count = _bal.FindSendMarCount(workOrder, "");
        IList<MaterialStockHistory> ms = _bal.FindSendMar(rows, page, workOrder, "");
        if (ms != null && ms.Count > 0)
        {
            for (int i = 0; i < ms.Count; i++)
            {
                ms[i].StockHouse = _bal.FindNameBySubCode(ms[i].StockHouse) == null ? ms[i].StockHouse : _bal.FindNameBySubCode(ms[i].StockHouse).SubName;
                ms[i].CustName = _bal.FindCustNameByCode(ms[i].CustName) == null ? ms[i].CustName : _bal.FindCustNameByCode(ms[i].CustName).NAME;
                ms[i].UpdatedBy = _bal.FindUserNameByCode(ms[i].UpdatedBy) == null ? ms[i].UpdatedBy : _bal.FindUserNameByCode(ms[i].UpdatedBy).UserName;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", count);
        map.Add("rows", ms);
        Context.Response.Write(JsonConvert.SerializeObject(map));



    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryInMaterial()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string msn = Context.Request.Form["msn"];
        string materialCode = Context.Request.Form["materialCode"];
        string batch = Context.Request.Form["batch"];
        string custName = Context.Request.Form["custName"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];


        PagingResult<MaterialStockHistory> objs = _bal.FindMaterialHistory("", "0", msn, materialCode, batch, custName, startTime, endTime, rows, page);
        if (objs != null && objs.ResultSet.Count > 0)
        {
            for (int i = 0; i < objs.ResultSet.Count; i++)
            {
                objs.ResultSet[i].StockHouse = _bal.FindNameBySubCode(objs.ResultSet[i].StockHouse) == null ? objs.ResultSet[i].StockHouse : _bal.FindNameBySubCode(objs.ResultSet[i].StockHouse).SubName;
                objs.ResultSet[i].CustName = _bal.FindCustNameByCode(objs.ResultSet[i].CustName) == null ? objs.ResultSet[i].CustName : _bal.FindCustNameByCode(objs.ResultSet[i].CustName).NAME;
                objs.ResultSet[i].UpdatedBy = _bal.FindUserNameByCode(objs.ResultSet[i].UpdatedBy) == null ? objs.ResultSet[i].UpdatedBy : _bal.FindUserNameByCode(objs.ResultSet[i].UpdatedBy).UserName;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", objs.ResultCount);
        map.Add("rows", objs.ResultSet);
        Context.Response.Write(JsonConvert.SerializeObject(map));

    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryOutMaterial()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string msn = Context.Request.Form["msn"];
        string materialCode = Context.Request.Form["materialCode"];
        string batch = Context.Request.Form["batch"];
        string custName = Context.Request.Form["custName"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];


        PagingResult<MaterialStockHistory> objs = _bal.FindMaterialHistory("", "1^3", msn, materialCode, batch, custName, startTime, endTime, rows, page);
        if (objs != null && objs.ResultSet.Count > 0)
        {
            for (int i = 0; i < objs.ResultSet.Count; i++)
            {
                objs.ResultSet[i].StockHouse = _bal.FindNameBySubCode(objs.ResultSet[i].StockHouse) == null ? objs.ResultSet[i].StockHouse : _bal.FindNameBySubCode(objs.ResultSet[i].StockHouse).SubName;
                objs.ResultSet[i].CustName = _bal.FindCustNameByCode(objs.ResultSet[i].CustName) == null ? objs.ResultSet[i].CustName : _bal.FindCustNameByCode(objs.ResultSet[i].CustName).NAME;
                objs.ResultSet[i].UpdatedBy = _bal.FindUserNameByCode(objs.ResultSet[i].UpdatedBy) == null ? objs.ResultSet[i].UpdatedBy : _bal.FindUserNameByCode(objs.ResultSet[i].UpdatedBy).UserName;
                objs.ResultSet[i].MaterialHandler = _bal.FindUserNameByCode(objs.ResultSet[i].MaterialHandler) == null ? objs.ResultSet[i].MaterialHandler : _bal.FindUserNameByCode(objs.ResultSet[i].MaterialHandler).UserName;
            }
        }
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", objs.ResultCount);
        map.Add("rows", objs.ResultSet);
        Context.Response.Write(JsonConvert.SerializeObject(map));

    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryInOutMaterial()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string materialCode = Context.Request.Form["materialCode"];
        string batch = Context.Request.Form["batch"];
        string custName = Context.Request.Form["custName"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        PagingResult<MaterialStock> objs = _bal.FindStockInfo("0^2", materialCode, batch, custName, startTime, endTime, rows, page);
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", objs.ResultCount);
        map.Add("rows", objs.ResultSet);
        Context.Response.Write(JsonConvert.SerializeObject(map));

    }

    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void ListCartonInfo()
    {
        string rows = Context.Request.Form["rows"];
        string page = Context.Request.Form["page"];
        string cartonNo = Context.Request.Form["cartonNo"];
        string startTime = Context.Request.Form["startTime"];
        string endTime = Context.Request.Form["endTime"];
        DateTime dtstart = DateTime.Today.AddDays(-7);
        DateTime dtend = DateTime.Now;
        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
        {
            dtstart = Convert.ToDateTime(startTime);
            dtend = Convert.ToDateTime(endTime);
        }
        PagingResult<CartonInfo> cts = _bal.FindCartonInfo(cartonNo, "", "", dtstart, dtend, rows, page);
        Dictionary<String, Object> map = new Dictionary<String, Object>();
        map.Add("total", cts.ResultCount);
        map.Add("rows", cts.ResultSet);
        Context.Response.Write(JsonConvert.SerializeObject(map));
        //StringBuilder sb = new StringBuilder();
        //sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        //sb.Append("<rows>");
        //string strstatus = string.Empty;
        //foreach (CartonInfo o in cts)
        //{
        //    sb.Append(string.Format("<row id='{0}'>", o.ID));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.CSN));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.OrderNumber));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.PartsdrawingCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QualityCode));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.QUANTITY.ToString()));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedBy));
        //    sb.Append(string.Format("<cell>{0}</cell>", o.UpdatedDate == null ? o.CreatedDate : o.UpdatedDate));
        //    sb.Append("</row>");
        //}
        //sb.Append("</rows>");
        //return sb.ToString();
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string FindWOByOrderAndPart(string orderNo, string partNo)
    {
        return _bal.FindWOByOrderAndPart(orderNo, partNo);

    }
    /// <summary>
    /// 查询物料未发数量和已发数量的比例
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryMaterialsProportion()
    {
        string res = "[{\"propertion\":[{\"data\":[";         
        IList<WorkOrder> wo = _bal.FindOnlineWOByKanBan("1000","1");
        int demandTotal = 0;
        int finishTotal = 0;
        int noFinishTotal = 0;
        if (wo != null && wo.Count > 0)
        {
            for (int i = 0; i < wo.Count; i++)
            {
                demandTotal += (int)wo[i].PlanQuantity;
                noFinishTotal += (int)(wo[i].PlanQuantity-(wo[i].MaterialQty==null?0:wo[i].MaterialQty));
                finishTotal += (int)(wo[i].MaterialQty == null ? 0 : wo[i].MaterialQty);

            }

        }
        float ffinishTotal = (float)(Math.Round(((double)finishTotal / (demandTotal==0?1:demandTotal))*10000)/100);
        float fnofinishTotal = 100 - ffinishTotal;
        res += "{\"name\":\"已发料\",\"y\":" + ffinishTotal + ",\"drilldown\":\"send\"},{\"name\":\"未发料\",\"y\":"
                + fnofinishTotal + ",\"drilldown\":\"nosend\"}]}],";
        res += "\"drilldown\":[{\"name\":\"send\",\"id\":\"send\",\"data\":[";
        for (int i = 0; i < wo.Count; i++)
        {
            float proportion = (float)(Math.Round(((double)((wo[i].MaterialQty == null ? 0 : wo[i].MaterialQty) / wo[i].PlanQuantity)) * 10000) / 100);
            res += "[\"";
            //res += wo[i].PartsdrawingCode + "\",\"" + (wo[i].MaterialQty == null ? 0 : wo[i].MaterialQty) + "/" + wo[i].PlanQuantity + "\"],";
            res += wo[i].PartsdrawingCode + "\"," + proportion + "],";
        }
        res = res.Substring(0, res.Length - 1);
        res += "]},{\"name\":\"nosend\",\"id\":\"nosend\",\"data\":[";

        for (int i = 0; i < wo.Count; i++)
        {           
            res += "[\"";
            int inosend = (int)(wo[i].PlanQuantity - (wo[i].MaterialQty == null ? 0 : wo[i].MaterialQty));
            float proportion = (float)(Math.Round(((double)(inosend / wo[i].PlanQuantity)) * 10000) / 100);
            res += wo[i].PartsdrawingCode + "\"," + proportion + "],";
           // res += wo[i].PartsdrawingCode + "\",\"" + inosend + "/" + wo[i].PlanQuantity + "\"],";
        }
        res = res.Substring(0, res.Length - 1);
        res += "]}]}]";
        //Dictionary<String, Object> map = new Dictionary<String, Object>();
        //map.Add("total", count);
        //map.Add("rows", wo);
        //Context.Response.Write(JsonConvert.SerializeObject(map));
        Context.Response.Write(res);
    }

    /// <summary>
    /// 查询产品不良数量和良品数量以及它们的的比例
    /// </summary>
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void QueryBackUpMaterialDetail()
    {        
        int count = _bal.FindOnlineWOCount();
        IList<WorkOrder> wo = _bal.FindOnlineWOByKanBan("1000", "1");
        if (wo != null && wo.Count > 0)
        {
            for (int i = 0; i < wo.Count; i++)
            {

                IList<MaterialStock> objs = _bal.QueryPrepareInfo(wo[i].WO);
                wo[i].MEMO = _bal.FindQMaterialByCustMaterial(wo[i].PartsdrawingCode);//钦纵料号 by tony add 2017-6-19
                if (objs != null && objs.Count > 0)
                {
                    BasCustom ibb = _bal.FindCustNameByCode(objs[0].CustName);
                    IList<BasBase> ibb1 = _bal.FindBaseBySubCode(objs[0].StockHouse);

                    if (ibb != null && !string.IsNullOrEmpty(ibb.NAME))
                    {
                        wo[i].CustName = ibb.NAME;
                    }
                    else
                    {

                        wo[i].CustName = objs[0].CustName;
                    }
                    if (ibb1 != null && ibb1.Count > 0)
                    {
                        wo[i].MachineName = ibb1[0].SubName;
                    }
                    else
                    {

                        wo[i].MachineName = objs[0].StockHouse;
                    }
                    wo[i].QUANTITY = objs.Count;

                }
                else
                {
                    wo[i].MachineName = "";
                    wo[i].QUANTITY = 0;
                }
            }

        }

        string res = "[{\"categories\":[{\"data\":";
        string strCategories = "[";
        string strPlanQty = "[";//需求数量
        string strQty = "[";//已发数量
        string strStockQty = "[";//库存数量
        bool bsub = false;
        for (int i = 0; i < wo.Count; i++)
        {
            strCategories += "\"" + wo[i].PartsdrawingCode + "\",";
            strQty += (wo[i].MaterialQty==null?0:wo[i].MaterialQty) + ",";
            strPlanQty += (wo[i].PlanQuantity == null ? 0 : wo[i].PlanQuantity) + ","; 
            strStockQty += (wo[i].QUANTITY == null ? 0 : wo[i].QUANTITY) + ",";
            bsub = true;
        }
        if (bsub)
        {
            strCategories = strCategories.Substring(0, strCategories.Length - 1);
            strQty = strQty.Substring(0, strQty.Length - 1);
            strPlanQty = strPlanQty.Substring(0, strPlanQty.Length - 1);
            strStockQty = strStockQty.Substring(0, strStockQty.Length - 1);
        }
        strCategories += "]";
        strQty += "]";
        strPlanQty += "]";
        strStockQty += "]";
        res += strCategories + "}],\"planqty\":[{\"data\":" + strPlanQty + "}],\"qty\":[{\"data\":" + strQty + "}],\"qtyrate\":[{\"data\":" + strStockQty + "}]}]";

        Context.Response.Write(res);
    }
    [WebMethod(true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string QueryInOutMaterialQty(string materialcode)
    { 
        PagingResult<MaterialStock> objs = _bal.FindStockInfo("0", materialcode, "", "", "", "", "5000", "1");
        if (objs != null && objs.ResultCount > 0)
        {
            return objs.ResultCount.ToString();
        }
        else
        {
            return "0";
        }
         

    }
}
