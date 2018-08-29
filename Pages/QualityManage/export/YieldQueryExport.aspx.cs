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
public partial class Pages_QualityManage_YieldQueryExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string custname = Request.QueryString["custname"];
        string partsdrawing = Request.QueryString["partsdrawing"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];

        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WorkOrder wo = new WorkOrder();
        wo.CustName = custname;
        wo.PartsdrawingCode = partsdrawing;
        if (!string.IsNullOrEmpty(starttime))
        {
            wo.StartTime = Convert.ToDateTime(starttime);
        }
        if (!string.IsNullOrEmpty(endtime))
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
        Sheet hssfSheet = hssfWorkbook.CreateSheet("YieldQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objhead = "客户名称,工件图号,工件名称,批次,产出数量,合格数量,不良数量,返工数量,让步数量,废品数量,合格率,不良率,返工率,让步率,废品率";
        for (int i = 0; i < objhead.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objhead.Split(',')[i]);            
        }
        for (int i = 1; i < objs.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objs[i - 1].CustName);
            cell = row.CreateCell(1);
            cell.SetCellValue(objs[i - 1].PartsdrawingCode);
            cell = row.CreateCell(2);
            cell.SetCellValue(objs[i - 1].ProductName);
            cell = row.CreateCell(3);
            cell.SetCellValue(objs[i - 1].BatchNumber);
            cell = row.CreateCell(4);           
            cell.SetCellValue((objs[i - 1].QUANTITY == null ? 0 : objs[i - 1].QUANTITY).ToString());
            int[] fails = _bal.FindYieldCountInfo("", objs[i - 1].PartsdrawingCode);
            int passcount = (int)(objs[i - 1].QUANTITY == null ? 0 : objs[i - 1].QUANTITY) - fails[0];
            string passrate = (Math.Round((double)(passcount * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";            
            string failrate = (Math.Round((double)(fails[0] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";            
            string returnrate = (Math.Round((double)(fails[1] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";            
            string secpassrate = (Math.Round((double)(fails[2] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";             
            string dicardrate = (Math.Round((double)(fails[3] * 100 / (objs[i - 1].QUANTITY == null ? 1 : objs[i - 1].QUANTITY)), 2)).ToString() + "%";
            cell = row.CreateCell(5);
            cell.SetCellValue(passcount.ToString());            
            cell = row.CreateCell(6);
            cell.SetCellValue(fails[0].ToString());
            cell = row.CreateCell(7);
            cell.SetCellValue(fails[1].ToString());
            cell = row.CreateCell(8);
            cell.SetCellValue(fails[2].ToString());
            cell = row.CreateCell(9);
            cell.SetCellValue(fails[3].ToString());
            cell = row.CreateCell(10);
            cell.SetCellValue(passrate);
            cell = row.CreateCell(11);
            cell.SetCellValue(failrate);
            cell = row.CreateCell(12);
            cell.SetCellValue(returnrate);
            cell = row.CreateCell(13);
            cell.SetCellValue(secpassrate);
            cell = row.CreateCell(14);
            cell.SetCellValue(dicardrate);

        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "YieldQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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