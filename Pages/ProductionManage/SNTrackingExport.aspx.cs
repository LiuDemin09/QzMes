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
public partial class Pages_ProductionManage_SNTrackingExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sn = Request.QueryString["sn"];       
        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        IList<TrackingHistory> objods = _bal.FindSNTrackingInfo(sn);
        if (objods == null || objods.Count == 0)
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
        string objs = "产品条码,来料条码,工单单号,零件图号,工序,机床名称,负责人,产品名称,时间,批次,数量,操作人";
        for (int i = 0; i < objs.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objs.Split(',')[i]);            
        }
        for (int i = 2; i <= objods.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objods[i - 2].PSN);
            cell = row.CreateCell(1);
            cell.SetCellValue(objods[i - 2].MSN);
            cell = row.CreateCell(2);
            cell.SetCellValue(objods[i - 2].WorkOrder);
            cell = row.CreateCell(3);
            cell.SetCellValue(objods[i - 2].PartsdrawingCode);
            cell = row.CreateCell(4);
            cell.SetCellValue(objods[i - 2].StationName);
            cell = row.CreateCell(5);
            cell.SetCellValue(objods[i - 2].MachineName);
            cell = row.CreateCell(6);
            cell.SetCellValue(objods[i - 2].UpdatedBy);
            cell = row.CreateCell(7);
            cell.SetCellValue(objods[i - 2].PartsName);
            cell = row.CreateCell(8);
            cell.SetCellValue(objods[i - 2].CreatedDate.ToString());
            cell = row.CreateCell(9);
            cell.SetCellValue(objods[i - 2].BatchNumber);
            cell = row.CreateCell(10);
            cell.SetCellValue(objods[i - 2].QUANTITY.ToString());
            cell = row.CreateCell(11);
            cell.SetCellValue(objods[i - 2].UpdatedBy);
        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "SNTrackingInfo" + DateTime.Now.ToString("yyyyMMddHHmmss");
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