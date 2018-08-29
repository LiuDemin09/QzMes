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
public partial class Pages_WorkOrderManage_TrackingWipExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string workorder = Request.QueryString["workorder"];
        string station = Request.QueryString["station"];
        string partsdrawingno = Request.QueryString["partsdrawingno"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];
        DateTime dtstart = DateTime.Today.AddDays(-100);
        DateTime dtend = DateTime.Now;
        if (!string.IsNullOrEmpty(starttime) && !string.IsNullOrEmpty(endtime))
        {
            dtstart = Convert.ToDateTime(starttime);
            dtend = Convert.ToDateTime(endtime);
        }
        
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<TrackingWip> woobjs = _bal.FindSNTrackingWIP(workorder, partsdrawingno, station, dtstart, dtend);
        WsSystem ws = new WsSystem();
        if (woobjs == null || woobjs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("SNTrackingWIPInfo");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objs = "产品条码,物料条码,工单单号,零件图号,批次号,工站,状态,工时,下一站,创建时间,操作人";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        if (woobjs != null)
        {
            for (int i = 2; i <= woobjs.Count + 1; i++)
            {
                row = hssfSheet.CreateRow(i);
                cell = row.CreateCell(0);
                cell.SetCellValue(woobjs[i - 2].PSN);
                cell = row.CreateCell(1);
                cell.SetCellValue(woobjs[i - 2].MSN);
                cell = row.CreateCell(2);
                cell.SetCellValue(woobjs[i - 2].WorkOrder);
                cell = row.CreateCell(3);
                cell.SetCellValue(woobjs[i - 2].PartsdrawingCode);
                cell = row.CreateCell(4);
                cell.SetCellValue(woobjs[i - 2].BatchNumber);
                cell = row.CreateCell(5);
                cell.SetCellValue(woobjs[i - 2].StationName);
                cell = row.CreateCell(6);
                cell.SetCellValue(woobjs[i - 2].STATUS);
                cell = row.CreateCell(7);
                cell.SetCellValue(woobjs[i - 2].TaskTime);
                cell = row.CreateCell(8);
                cell.SetCellValue(woobjs[i - 2].NextStation);
                cell = row.CreateCell(9);
                cell.SetCellValue(woobjs[i - 2].CreatedDate.ToString());
                cell = row.CreateCell(10);
                cell.SetCellValue(woobjs[i - 2].UpdatedBy); 
            }
        }
         
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "SNTrackingWIPInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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