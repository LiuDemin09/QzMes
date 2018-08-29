using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using NPOI;
using NPOI.HSSF;
using NPOI.SS.Util;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;
using DAL;
using BLL;
public partial class Pages_WorkOrderManage_WorkOrderMainExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string workorder = Request.QueryString["workorder"];
        //string status = Request.QueryString["status"];
        string partsdrawingno = Request.QueryString["partcode"];
        string order = Request.QueryString["orderno"];
        //string starttime = Request.QueryString["starttime"];
        //string endtime = Request.QueryString["endtime"];
        //DateTime dtstart = DateTime.Today.AddDays(-100);
        //DateTime dtend = DateTime.Now;
        //if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        //{
        //    dtstart = Convert.ToDateTime(starttime);
        //    dtend = Convert.ToDateTime(endtime);
        //}
        //WorkOrder wo = new WorkOrder();
        //wo.WO = workorder;
        //wo.STATUS = status=="4"?"":status;
        //wo.PartsdrawingCode = partsdrawingno;
        //wo.OrderNumber = order;
        //wo.StartTime = dtstart;
        //wo.EndTime = dtend;
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<OrderDetail> objs = _bal.FindOrderInfo(order, partsdrawingno, "1");
        //IList<WorkOrder> woobjs = _bal.FindWorkOrderInfo(wo);
        WsSystem ws = new WsSystem();
        if (objs == null || objs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("OrderInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string strobjs = "订单单号,零件图号,状态,客户名称,投产总数,产品名称,交付时间";
        for (int i = 0; i < strobjs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(strobjs.Split(',')[i]);            
        }
        if (objs != null)
        {
            for (int i = 2; i <= objs.Count + 1; i++)
            {
                row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(objs[i - 2].OrderNo);
                cell = row.CreateCell(1);
                cell.SetCellValue(objs[i - 2].PartsdrawingCode);
                cell = row.CreateCell(2);
                cell.SetCellValue(objs[i - 2].MEMO);
                cell = row.CreateCell(3);
                cell.SetCellValue(objs[i - 2].CustName);
                cell = row.CreateCell(4);
                cell.SetCellValue(objs[i - 2].OrderQuantity.ToString());
                cell = row.CreateCell(5);
                cell.SetCellValue(objs[i - 2].ProductName);
                cell = row.CreateCell(6);
                cell.SetCellValue(objs[i - 2].OutDate.ToString());
                
            }
        }
         
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "OrderInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xls", fileName));
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.AddHeader("Content-Transfer-Encoding", "binary");
        Response.ContentType = "application/octet-stream";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
        Response.BinaryWrite(file.GetBuffer());
        Response.Flush();
        Response.End();
    }
}