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
public partial class Pages_WorkOrderManage_ProductMarQueryExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string workorder = Request.QueryString["workorder"];
        string partsdrawing = Request.QueryString["partsdrawing"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];
        if(workorder== "undefined")
        {
            workorder = "";
        }
        if(partsdrawing== "undefined")
        {
            partsdrawing = "";
        }
        if(starttime== "undefined")
        {
            starttime = "";
        }
        if(endtime== "undefined")
        {
            endtime = "";
        }
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WorkOrder wo = new WorkOrder();
        wo.WO = workorder;
        wo.PartsdrawingCode = partsdrawing;
        if (!string.IsNullOrEmpty(starttime)&&starttime!= "undefined")
        {
            wo.StartTime = Convert.ToDateTime(starttime);
        }
         
        if (!string.IsNullOrEmpty(endtime) && starttime != "undefined")
        {
            wo.EndTime = Convert.ToDateTime(endtime);
        }
        IList<WorkOrder> objs = _bal.FindWorkOrderInfo(wo);       
        if (objs == null || objs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("ProductMarQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objhead = @"订单号码,工单号码,零件图号,产品名称,生产人员,机床名称
            , 计划数量, 产出数量,合格数量,废品数量,额定工时,额定总工时,实际总工时";
        for (int i = 0; i < objhead.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objhead.Split(',')[i]);            
        }
        for (int i = 1; i < objs.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objs[i - 1].OrderNumber);
            cell = row.CreateCell(1);
            cell.SetCellValue(objs[i - 1].WO);
            cell = row.CreateCell(2);
            cell.SetCellValue(objs[i - 1].PartsdrawingCode);
            cell = row.CreateCell(3);
            cell.SetCellValue(objs[i - 1].ProductName);
            cell = row.CreateCell(4);
            cell.SetCellValue(objs[i - 1].WorkerName);
            cell = row.CreateCell(5);
            cell.SetCellValue(objs[i - 1].MachineName);
            cell = row.CreateCell(6);
            cell.SetCellValue(objs[i - 1].PlanQuantity.ToString());
            cell = row.CreateCell(7);
            cell.SetCellValue(objs[i - 1].QUANTITY.ToString());
            cell = row.CreateCell(8); 

            int[] fails = _bal.FindYieldCountInfo("", objs[i - 1].PartsdrawingCode);
            int passcount = (int)(objs[i - 1].QUANTITY == null ? 0 : objs[i - 1].QUANTITY) - fails[0];
            //string passrate = (Math.Round((double)(passcount * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";            
            //string failrate = (Math.Round((double)(fails[0] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";            
            //string returnrate = (Math.Round((double)(fails[1] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";            
            //string secpassrate = (Math.Round((double)(fails[2] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";             
            //string dicardrate = (Math.Round((double)(fails[3] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";
            cell.SetCellValue(passcount.ToString());

            cell = row.CreateCell(9);
            cell.SetCellValue(fails[3].ToString());
            cell = row.CreateCell(10);
            cell.SetCellValue(objs[i - 1].UnitTime.ToString());
            cell = row.CreateCell(11); 
            cell.SetCellValue((objs[i - 1].QUANTITY * objs[i - 1].UnitTime).ToString());
            cell = row.CreateCell(12);
            cell.SetCellValue(_bal.FindActualTotalUnitTime(objs[i - 1].WO, objs[i - 1].PartsdrawingCode));
            
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "ProductMarQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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