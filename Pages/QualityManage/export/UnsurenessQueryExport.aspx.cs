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
public partial class Pages_QualityManage_UnsurenessQueryExport : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string psn = Request.QueryString["psn"];
        string partsdrawing = Request.QueryString["partsdrawing"];
        string wo = Request.QueryString["wo"];
        string status = Request.QueryString["status"];
        string starttime = Request.QueryString["starttime"];
        string endtime = Request.QueryString["endtime"];

        SystemBO _bal = BLLFactory.GetBal<SystemBO>(userInfo);
        WsSystem ws = new WsSystem();
        UnsurenessHistory uh = new UnsurenessHistory();
        uh.PSN = psn;
        uh.WorkOrder = wo;
        uh.STATUS = status;
        uh.PartsdrawingCode = partsdrawing;
        
        if (!string.IsNullOrEmpty(starttime))
        {
            uh.CreatedDate = Convert.ToDateTime(starttime);
        }
        if (!string.IsNullOrEmpty(endtime))
        {
            uh.UpdatedDate = Convert.ToDateTime(endtime);
        }
        IList<UnsurenessHistory> objs = _bal.FindUnsurenessHistory(uh);
        if (objs == null || objs.Count == 0)
        {
            Response.Write("no data");
            return;
        }

        HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
        Row row = null;
        Cell cell = null;
        Sheet hssfSheet = hssfWorkbook.CreateSheet("UnsurenessQuery");
        row = hssfSheet.CreateRow(0);
        //填充头
        string objhead = "产品条码,工单号码,不良项,状态,加工工序,数量,来料条码,工件图号,工件名称,生产批号,操作人,时间";
        for (int i = 0; i < objhead.Split(',').Length; i++)
        {
            cell = row.CreateCell(i);
            cell.SetCellValue(objhead.Split(',')[i]);            
        }
        for (int i = 1; i < objs.Count + 1; i++)
        {
            row = hssfSheet.CreateRow(i);
            cell = row.CreateCell(0);
            cell.SetCellValue(objs[i - 1].PSN);
            cell = row.CreateCell(1);
            cell.SetCellValue(objs[i - 1].WorkOrder);
            cell = row.CreateCell(2);
            cell.SetCellValue(objs[i - 1].FailMemo);
            cell = row.CreateCell(3);
            cell.SetCellValue(objs[i - 1].MEMO);
            cell = row.CreateCell(4);           
            cell.SetCellValue(objs[i - 1].StationName);
            cell = row.CreateCell(5);
            cell.SetCellValue(objs[i - 1].QUANTITY.ToString());           
            cell = row.CreateCell(6);
            cell.SetCellValue(objs[i-1].MSN);
            cell = row.CreateCell(7);
            cell.SetCellValue(objs[i - 1].PartsdrawingCode);
            cell = row.CreateCell(8);
            cell.SetCellValue(objs[i - 1].ProductName);
            cell = row.CreateCell(9);
            cell.SetCellValue(objs[i - 1].BatchNumber);
            cell = row.CreateCell(10);
            cell.SetCellValue(ws.FindUserNameByCode(objs[i - 1].UpdatedBy));
            cell = row.CreateCell(11);
            cell.SetCellValue(objs[i - 1].CreatedDate.ToString());

        }
        MemoryStream file = new MemoryStream();
        hssfWorkbook.Write(file);
        String fileName = "UnsurenessQuery" + DateTime.Now.ToString("yyyyMMddHHmmss");
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